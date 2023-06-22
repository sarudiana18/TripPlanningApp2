import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { TripParams } from '../_models/tripParams';
import { AtractieTuristica } from '../_models/atractieturistica';
import { Hotel } from '../_models/hotel';
import { ActivatedRoute, Router } from '@angular/router';
import { TripPlanningService } from '../_services/tripplanning.service';
import { Parc } from '../_models/parc';
import { Restaurant } from '../_models/restaurant';
import { getPaginatedResult, getPaginationHeaders } from '../_services/paginationHelper';
import { map } from 'rxjs';

@Component({
  selector: 'app-trip-details',
  templateUrl: './trip-details.component.html',
  styleUrls: ['./trip-details.component.css']
})
export class TripDetailsComponent implements OnInit {

  pagination: Pagination | undefined;
  startDate: any;
  endDate: any;
  tripParams =  {} as TripParams;
  atractiiTuristice: AtractieTuristica[] = [];
  hoteluri:Hotel[] = [];
  parcuri: Parc[] = [];
  restaurante: Restaurant[] = [];
  origin: any;
  cityIdFromParams: number = 0;
  destination: any;
  public directionsResponse: any;
  displayForAddingNewHotel: boolean = false;
  newHotel: Hotel = {
    nume: '',
    adresa:'',
    pricePerNight: 0,
    pricePerNightCameraTripla:0,
    cityId:0,
  };
  validationErrors: string[] | undefined;

  displayForAddingNewAttraction: boolean = false;
  newAtractie: AtractieTuristica = {
    cityId: 0,
    nume:'',
    descriere:'',
    adresa:'',
  };
  displayForAddingNewParc: boolean = false;
  newParc: Parc = {
     cityId: 0,
    nume:'',
    adresa:'',
  };
  displayForAddingNewRestaurant: boolean = false;
  newRestaurant: Restaurant = {
    cityId: 0,
    nume:'',
    specific:'',
    adresa:'',
  };
  constructor(private route: ActivatedRoute, private tripPlanningService : TripPlanningService) { 
      
    }

  ngOnInit(): void {
    this.route.paramMap
    .pipe(map(() => window.history.state)).subscribe(res=>{
          this.tripParams = res;
          console.log(res);
          if( this.tripParams.destinationCity.id){
            this.cityIdFromParams =  this.tripParams.destinationCity.id;
            this.tripPlanningService.getAtractiiTuristiceByCityId( this.tripParams.destinationCity.id).subscribe({
              next: response => {
                this.atractiiTuristice = response;
              }
            });
            this.tripPlanningService.getHoteluriByCityIdAndByBugetAndByNoOfNightsAndByNrPersoane( this.tripParams.destinationCity.id,
              this.tripParams.buget, this.tripParams.nrNopti, this.tripParams.nrPersoane).subscribe({
              next: response => {
                this.hoteluri = response;
              }
            });
            this.tripPlanningService.getRestauranteByCityId( this.tripParams.destinationCity.id).subscribe({
              next: response => {
                this.restaurante = response;
              }
            });
            this.tripPlanningService.getParcuriByCityId( this.tripParams.destinationCity.id).subscribe({
              next: response => {
                this.parcuri = response;
              }
            });
          } 
          if(this.tripParams.sourceCity && this.tripParams.destinationCity){
            this.origin = { lat: this.tripParams.sourceCity.latitude, lng: this.tripParams.sourceCity.longitude };
            this.destination = { lat: this.tripParams.destinationCity.latitude, lng: this.tripParams.destinationCity.longitude };
          } 
        });

  }

  pageChanged(event: any) {
    if (this.tripParams && this.tripParams?.pageNumber !== event.page) {
      this.tripParams.pageNumber = event.page;
      //this.memberService.setUserParams(this.userParams);
      //this.loadMembers();
    }
  }
  addAtractie(){
    this.displayForAddingNewAttraction = true;
  }
  submitAtractieTuristica(){
    this.newAtractie.cityId = this.cityIdFromParams;
    
    // Add the new review to the reviews array
    this.tripPlanningService.addNewAtractieTuristica(this.newAtractie).subscribe({
      next: response => {
        this.atractiiTuristice.push(response);
        this.displayForAddingNewAttraction = false;
        this.newAtractie = {
          cityId: 0,
          nume:'',
          descriere:'',
          adresa:'',
        };
      },
      error: error => {
        this.validationErrors = error
      } 
    })
  }
  addHotel(){
    this.displayForAddingNewHotel = true;
  }
  submitHotel(){
    this.newHotel.cityId = this.cityIdFromParams;
    
    // Add the new review to the reviews array

    this.tripPlanningService.addNewHotel(this.newHotel).subscribe({
      next: response => {
        this.hoteluri.push(response);
        this.displayForAddingNewHotel=false;
        // Clear the form fields
        this.newHotel = {
          nume: '',
          adresa:'',
          pricePerNight: 0,
          pricePerNightCameraTripla:0,
          cityId:0,
        };
      },
      error: error => {
        this.validationErrors = error
      } 
    })
    
  }

  addParc(){
    this.displayForAddingNewParc = true;
  }
  submitParc(){
    this.newParc.cityId= this.cityIdFromParams;
    
    // Add the new review to the reviews array
    this.tripPlanningService.addNewParc(this.newParc).subscribe({
      next: response => {
        this.parcuri.push(response);
        this.displayForAddingNewParc = false;
        this.newParc = {
          cityId: 0,
          nume:'',
          adresa:'',
        };
      },
      error: error => {
        this.validationErrors = error
      } 
    })
  }

  addRestaurant(){
    this.displayForAddingNewRestaurant = true;
  }
  submitRestaurant(){
    this.newRestaurant.cityId = this.cityIdFromParams;
    
    // Add the new review to the reviews array
    this.tripPlanningService.addNewRestaurant(this.newRestaurant).subscribe({
      next: response => {
        this.restaurante.push(response);
        this.displayForAddingNewRestaurant = false;
        this.newRestaurant = {
          cityId: 0,
          nume:'',
          specific :'',
          adresa:'',
        };
      },
      error: error => {
        this.validationErrors = error
      } 
    })
  }
}
