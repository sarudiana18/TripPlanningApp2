import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'primeng/api';
import { take } from 'rxjs';
import { Parc } from 'src/app/_models/parc';
import { Review } from 'src/app/_models/review';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { TripPlanningService } from 'src/app/_services/tripplanning.service';

@Component({
  selector: 'app-parc-details',
  templateUrl: './parc-details.component.html',
  styleUrls: ['./parc-details.component.css']
})
export class ParcDetailsComponent implements OnInit {

  @ViewChild('parcTabs', {static: true}) parcTabs?: TabsetComponent;
  parc: Parc = {} as Parc;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];
  activeTab?: TabDirective;
  reviews: Review[] = [];
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
      let parcId = params['parcId'];
      if(parcId){
        this.tripPlanningService.getParcById(parcId).subscribe({
          next: response => {
            this.parc = response;
            this.galleryImages = this.getImages();
          }
        })
      } 
    });

    this.route.queryParams.subscribe({
      next: params => {
        params['tab'] && this.selectTab(params['tab'])
      }
    })

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }

  getImages() {
    if (!this.parc) return [];
    const imageUrls = [];
    if(this.parc.photos){
      for (const photo of this.parc.photos) {
        imageUrls.push({
          small: photo.url,
          medium: photo.url,
          big: photo.url
        })
      }
    }
    return imageUrls;
  }

  selectTab(heading: string) {
    if (this.parcTabs) {
      this.parcTabs.tabs.find(x => x.heading === heading)!.active = true
    }
  }
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }
}
