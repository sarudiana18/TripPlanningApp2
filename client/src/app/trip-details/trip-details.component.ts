import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { TripParams } from '../_models/tripParams';
import { AtractieTuristica, AtractieTuristicaFilter } from '../_models/atractieturistica';
import { Hotel, HotelFilter } from '../_models/hotel';
import { ActivatedRoute, Router } from '@angular/router';
import { TripPlanningService } from '../_services/tripplanning.service';
import { Parc, ParcFilter } from '../_models/parc';
import { Restaurant, RestaurantFilter } from '../_models/restaurant';
import { BehaviorSubject, map } from 'rxjs';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-trip-details',
  templateUrl: './trip-details.component.html',
  styleUrls: ['./trip-details.component.css']
})
export class TripDetailsComponent implements OnInit {

  private currentTripDetailsParams = new BehaviorSubject<TripParams | null>(null);
  currentTripDetails$ = this.currentTripDetailsParams.asObservable();

  paginationAtractii: Pagination | undefined;
  paginationHotels: Pagination | undefined;

  paginationRestaurante: Pagination | undefined;

  paginationParcuri: Pagination | undefined;

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

  hotelFilter = new HotelFilter();
  atractiiFilter = new AtractieTuristicaFilter();
  restaurantFilter = new RestaurantFilter();
  parcFilter = new ParcFilter();

  constructor(private route: ActivatedRoute, private tripPlanningService : TripPlanningService) { 
      
    }

  ngOnInit(): void {
    this.route.paramMap
    .pipe(map(() => window.history.state)).subscribe(res=>{
          
          const tripParamsString = localStorage.getItem('tripDetailsParams');
          if(tripParamsString){
            this.tripParams = JSON.parse(tripParamsString);
          }
          else{
            this.tripParams = res;
          }
          this.hotelFilter.buget = this.tripParams.buget;
          this.hotelFilter.nrNopti = this.tripParams.nrNopti;
          this.hotelFilter.nrPersoane = this.tripParams.nrPersoane;
          this.hotelFilter.cityId = this.tripParams.destinationCity.id;
          this.atractiiFilter.cityId = this.tripParams.destinationCity.id;
          this.parcFilter.cityId  = this.tripParams.destinationCity.id;
          this.restaurantFilter.cityId = this.tripParams.destinationCity.id;
          
          if( this.tripParams.destinationCity.id){
            this.cityIdFromParams =  this.tripParams.destinationCity.id;
            
            this.getAtractiiTuristiceFiltered();
            this.getHotels();
            this.getRestaurante();
            this.getParcuri();
          } 
          if(this.tripParams.sourceCity && this.tripParams.destinationCity){
            this.origin = { lat: parseFloat(this.tripParams.sourceCity.latitude.toString()), lng: parseFloat(this.tripParams.sourceCity.longitude.toString()) };
            this.destination = { lat: parseFloat(this.tripParams.destinationCity.latitude.toString()), lng: parseFloat(this.tripParams.destinationCity.longitude.toString()) };
          } 
        });

  }
  addAtractie(){
    this.displayForAddingNewAttraction = true;
  }
  submitAtractieTuristica(){
    this.newAtractie.cityId = this.cityIdFromParams;
    
    // Add the new review to the reviews array
    this.tripPlanningService.addNewAtractieTuristica(this.newAtractie).subscribe({
      next: response => {
        //this.atractiiTuristice.push(response);
        this.getAtractiiTuristiceFiltered();
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
        //this.hoteluri.push(response);
        this.getHotels();
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
        //this.parcuri.push(response);
        this.getParcuri();
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
        //this.restaurante.push(response);
        this.getRestaurante();
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

  getHotels(){
    this.tripPlanningService.getHotels(this.hotelFilter).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.hoteluri = response.result;
          this.paginationHotels = response.pagination;
        }
      }
    })
  }
  clearFiltersHotels(){
    this.hotelFilter = new HotelFilter();
    this.hotelFilter.buget = this.tripParams.buget;
    this.hotelFilter.nrNopti = this.tripParams.nrNopti;
    this.hotelFilter.nrPersoane = this.tripParams.nrPersoane;
    this.hotelFilter.cityId = this.tripParams.destinationCity.id;
    this.getHotels();
  }
  pageChangedHotels(event: any) {
    if (this.hotelFilter && this.hotelFilter?.pageNumber !== event.page) {
      this.hotelFilter.pageNumber = event.page;
      this.getHotels();
    }
  }
  getAtractiiTuristiceFiltered(){
    this.tripPlanningService.getAtractiiTuristice(this.atractiiFilter).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.atractiiTuristice = response.result;
          this.paginationAtractii = response.pagination;
        }
      }
    })
  }
  clearFiltersAtractii(){
    this.atractiiFilter = new AtractieTuristicaFilter();
    this.atractiiFilter.cityId = this.tripParams.destinationCity.id;
    this.getAtractiiTuristiceFiltered();
  }

  pageChangedAtractii(event: any) {
    if (this.atractiiFilter && this.atractiiFilter?.pageNumber !== event.page) {
      this.atractiiFilter.pageNumber = event.page;
      this.getAtractiiTuristiceFiltered();
    }
  }

  getRestaurante(){
    this.tripPlanningService.getRestaurante(this.restaurantFilter).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.restaurante = response.result;
          this.paginationRestaurante = response.pagination;
        }
      }
    })
  }
  clearFiltersRestaurante(){
    this.restaurantFilter = new RestaurantFilter();
    this.restaurantFilter.cityId = this.tripParams.destinationCity.id;
    this.getRestaurante();
  }

  pageChangedRestaurante(event: any) {
    if (this.restaurantFilter && this.restaurantFilter?.pageNumber !== event.page) {
      this.restaurantFilter.pageNumber = event.page;
      this.getRestaurante();
    }
  }
  getParcuri(){
    this.tripPlanningService.getParcuri(this.parcFilter).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.parcuri = response.result;
          this.paginationParcuri = response.pagination;
        }
      }
    })
  }
  clearFiltersParcuri(){
    this.parcFilter = new HotelFilter();
    this.parcFilter.cityId = this.tripParams.destinationCity.id;
    this.getParcuri();
  }
  pageChangedParcuri(event: any) {
    if (this.parcFilter && this.parcFilter?.pageNumber !== event.page) {
      this.parcFilter.pageNumber = event.page;
      this.getParcuri();
    }
  }
}
