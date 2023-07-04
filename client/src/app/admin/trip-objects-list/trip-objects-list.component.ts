import { Component, OnInit } from '@angular/core';
import { Pagination } from '../../_models/pagination';
import { TripParams } from '../../_models/tripParams';
import { AtractieTuristica, AtractieTuristicaFilter } from '../../_models/atractieturistica';
import { Hotel, HotelFilter } from '../../_models/hotel';
import { TripPlanningService } from '../../_services/tripplanning.service';
import { Parc, ParcFilter } from '../../_models/parc';
import { Restaurant, RestaurantFilter } from '../../_models/restaurant';
import { City } from 'src/app/_models/city';
import { Country } from 'src/app/_models/country';
import { State } from 'src/app/_models/state';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-trip-objects-list',
  templateUrl: './trip-objects-list.component.html',
  styleUrls: ['./trip-objects-list.component.css']
})
export class TripObjectListComponent implements OnInit {

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

  cities: City[] = [];
  countries: Country[] = [];
  states: State[] = [];
  showCities: boolean = false;

  constructor(private tripPlanningService : TripPlanningService, private confirmationService: ConfirmationService) { 
      
    }

  ngOnInit(): void {
      this.loadObjectsFromCache();
      this.loadCountries();
  }

  loadObjectsFromCache(){
    const tripObjectDetailsParams = localStorage.getItem('tripObjectDetailsParams');
    if(tripObjectDetailsParams){
      this.tripParams = JSON.parse(tripObjectDetailsParams);
      if((this.states == null || this.states.length == 0) && this.tripParams.destinationState?.id){
        this.loadStates();
      }
      if((this.cities == null || this.cities.length == 0) && this.tripParams.destinationCity?.id){
        this.loadCities();
      }
    }
    
    this.loadObjects();
  }
  loadObjects(){
    if(this.tripParams.destinationCity?.id){
      this.atractiiFilter.cityId = this.tripParams.destinationCity.id;
      this.hotelFilter.cityId = this.tripParams.destinationCity.id;
      this.restaurantFilter.cityId = this.tripParams.destinationCity.id;
      this.parcFilter.cityId = this.tripParams.destinationCity.id;
      localStorage.setItem('tripObjectDetailsParams', JSON.stringify(this.tripParams));
    }
    this.getAtractiiTuristiceFiltered();
    this.getHotels();
    this.getRestaurante();
    this.getParcuri();
  }
  addAtractie(){
    this.displayForAddingNewAttraction = true;
  }
  submitAtractieTuristica(){
    this.newAtractie.cityId = this.tripParams.destinationCity.id;
    
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
    this.newHotel.cityId = this.tripParams.destinationCity.id;
    
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
    this.newParc.cityId= this.tripParams.destinationCity.id;
    
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
    this.newRestaurant.cityId = this.tripParams.destinationCity.id;
    
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
    if(this.tripParams.destinationCity?.id){
      this.hotelFilter.cityId = this.tripParams.destinationCity.id;
    }
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
    if(this.tripParams.destinationCity?.id){
      this.atractiiFilter.cityId = this.tripParams.destinationCity.id;
    }    
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
    if(this.tripParams.destinationCity?.id){
      this.restaurantFilter.cityId = this.tripParams.destinationCity.id;
    } 
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
    if(this.tripParams.destinationCity?.id){
      this.parcFilter.cityId = this.tripParams.destinationCity.id;
    }
    this.getParcuri();
  }
  pageChangedParcuri(event: any) {
    if (this.parcFilter && this.parcFilter?.pageNumber !== event.page) {
      this.parcFilter.pageNumber = event.page;
      this.getParcuri();
    }
  }

  loadCountries() {
    this.tripPlanningService.getCountries().subscribe({
      next: response => {
        this.countries = response;
      }
    })
  }
  loadStates() {
    if (this.tripParams?.destinationCountry) {
      this.tripPlanningService.getStates(this.tripParams?.destinationCountry.id).subscribe({
        next: response => {
          this.states = response;
        }
      })
    }
  }

  loadCities() {
    if (this.tripParams?.destinationState) {
      this.tripPlanningService.getCities(this.tripParams.destinationState.id).subscribe({
        next: response => {
          this.cities = response;
          if (this.cities.length == 0 ) {
            this.showCities = false;
            this.tripParams.destinationCity = {} as City;
            this.tripParams.destinationCity.name = this.tripParams.destinationState.name;
            this.tripParams.destinationCity.id = this.tripParams.destinationState.id;
            localStorage.setItem('tripObjectDetailsParams', JSON.stringify(this.tripParams));
            this.loadObjects();
          }
          else {
            this.showCities = true;
          }
        }
      })
    }
  }
  deleteObject(path: string, objectId: number | undefined){
    this.confirmationService.confirm({
      message: 'Sunteti sigur ca doriti sa stergeti acest obiect?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel:"Da",
      rejectLabel:"Nu",
      accept: () => {
        if(objectId){
          this.tripPlanningService.removeObject(path, objectId).subscribe({
            next: _ => {
              if (path == 'atractiiTuristice') {
                this.getAtractiiTuristiceFiltered();
                // this.atractiiTuristice = this.atractiiTuristice.filter(x => x.id !== objectId);
              }
              else if(path == 'hoteluri'){
                this.getHotels();
                // this.hoteluri = this.hoteluri.filter(x => x.id !== objectId);
              }
              else if(path == 'restaurante'){
                this.getRestaurante();
                // this.restaurante = this.restaurante.filter(x => x.id !== objectId);
              }
              else if(path == 'parcuri'){
    
                this.getParcuri();
                // this.parcuri = this.parcuri.filter(x => x.id !== objectId);
              }
            }
          })
        }
      },
  });

  }
}
