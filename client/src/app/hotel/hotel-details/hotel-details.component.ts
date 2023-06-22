import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'primeng/api';
import { take } from 'rxjs';
import { Hotel } from 'src/app/_models/hotel';
import { Review } from 'src/app/_models/review';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { TripPlanningService } from 'src/app/_services/tripplanning.service';

@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css']
})
export class HotelDetailsComponent implements OnInit {

  @ViewChild('hotelTabs', {static: true}) hotelTabs?: TabsetComponent;
  hotel: Hotel = {} as Hotel;
  startDate: string = "";
  endDate: string = "";

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
      let hotelId = params['hotelId'];
      this.startDate = params['startDate'];
      this.endDate = params['endDate'];
      if(hotelId){
        this.tripPlanningService.getHotelById(hotelId).subscribe({
          next: response => {
            this.hotel = response;
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
    if (!this.hotel) return [];
    const imageUrls = [];
    if(this.hotel.photos){
      for (const photo of this.hotel.photos) {
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
    if (this.hotelTabs) {
      this.hotelTabs.tabs.find(x => x.heading === heading)!.active = true
    }
  }
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }
  redirectToBooking(){
    const params = new URLSearchParams();
    params.append('ss', this.hotel.nume);
    console.log(this.startDate, this.endDate);
    let startDate = this.getDateOnly(this.startDate);
    let endDate = this.getDateOnly(this.endDate)

    if(startDate && endDate){
      params.append('checkin', startDate);
      params.append('checkout', endDate);
    }
    
    const bookingUrl = `https://www.booking.com/searchresults.html?${params.toString()}`;
    window.location.href = bookingUrl;  
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    let theDob = new Date(dob);
    return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset()))
      .toISOString().slice(0,10);
  }
}
