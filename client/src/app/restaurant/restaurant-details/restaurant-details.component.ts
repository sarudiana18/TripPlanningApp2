import { query } from '@angular/animations';
import { Component, OnInit, Query, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'primeng/api';
import { take } from 'rxjs';
import { Restaurant } from 'src/app/_models/restaurant';
import { Review } from 'src/app/_models/review';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { TripPlanningService } from 'src/app/_services/tripplanning.service';
import * as queryString from 'querystring'

@Component({
  selector: 'app-restaurant-details',
  templateUrl: './restaurant-details.component.html',
  styleUrls: ['./restaurant-details.component.css']
})
export class RestaurantDetailsComponent implements OnInit {

  @ViewChild('restaurantTabs', {static: true}) restaurantTabs?: TabsetComponent;
  restaurant: Restaurant = {} as Restaurant;
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
      let restaurantId = params['restaurantId'];
      if(restaurantId){
        this.tripPlanningService.getRestaurantById(restaurantId).subscribe({
          next: response => {
            this.restaurant = response;
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
    if (!this.restaurant) return [];
    const imageUrls = [];
    if(this.restaurant.photos){
      for (const photo of this.restaurant.photos) {
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
    if (this.restaurantTabs) {
      this.restaurantTabs.tabs.find(x => x.heading === heading)!.active = true
    }
  }
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }
}
