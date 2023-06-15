import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { take } from 'rxjs';
import { AtractieTuristica } from 'src/app/_models/atractieturistica';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { TripPlanningService } from 'src/app/_services/tripplanning.service';

@Component({
  selector: 'app-atractie-turistica-details',
  templateUrl: './atractie-turistica-details.component.html',
  styleUrls: ['./atractie-turistica-details.component.css']
})
export class AtractieTuristicaDetailsComponent implements OnInit {

  @ViewChild('atractieTuristicaTabs', {static: true}) atractieTuristicaTabs?: TabsetComponent;
  atractieTuristica: AtractieTuristica = {} as AtractieTuristica;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];
  activeTab?: TabDirective;
  user?: User;

  constructor(private accountService: AccountService, private route: ActivatedRoute, 
      private router: Router, private tripPlanningService: TripPlanningService) {
          this.accountService.currentUser$.pipe(take(1)).subscribe({
            next: user => {
              if (user) this.user = user;
            }
          });
          this.router.routeReuseStrategy.shouldReuseRoute = () => false;
       }

  ngOnInit(): void {
    
    this.route.params.subscribe(params=>{
      let atractieId = params['atractieId'];
      if(atractieId){
        this.tripPlanningService.getAtractieTuristicaById(atractieId).subscribe({
          next: response => {
            this.atractieTuristica = response;
            this.galleryImages = this.getImages();
          }
        })
      } 
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '600px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }

  getImages() {
    if (!this.atractieTuristica) return [];
    const imageUrls = [];
    if(this.atractieTuristica.photos){
      for (const photo of this.atractieTuristica.photos) {
        imageUrls.push({
          small: photo.url,
          medium: photo.url,
          big: photo.url
        })
      }
    } 
    return imageUrls;
  }
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }
}
