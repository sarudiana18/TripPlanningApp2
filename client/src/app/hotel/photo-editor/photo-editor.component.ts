import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Hotel } from 'src/app/_models/hotel';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/member.service';
import { TripPlanningService } from 'src/app/_services/tripplanning.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor-hotel',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponentHotel implements OnInit {
  @Input() object: Hotel | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;

  constructor(private accountService: AccountService, private memberService: MembersService,
    private tripPlanningService: TripPlanningService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user
      }
    })
   }

  ngOnInit(): void {
    this.initializeUploader();
    
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: Photo) {
    this.tripPlanningService.setMainPhoto(photo.id,'hoteluri').subscribe({
      next: () => {
        if (this.user && this.object) {
          this.object.photoUrl = photo.url;
          this.object.photos?.forEach((p: { isMain: boolean; id: number; }) => {
            if (p.isMain) p.isMain = false;
            if (p.id === photo.id) p.isMain = true;
          })
        }
      }
    })
  }

  deletePhoto(photoId: number) {
    this.tripPlanningService.deletePhoto(photoId,'hoteluri').subscribe({
      next: _ => {
        if (this.object) {
          this.object.photos = this.object.photos?.filter((x: { id: number; }) => x.id !== photoId);
        }
      }
    })
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'hoteluri/add-photo/'+this.object?.id,
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);
        if (photo.isMain && this.user && this.object) {
          this.object.photoUrl = photo.url;
        }
        this.object?.photos?.push(photo);
      }
    }
  }

}
