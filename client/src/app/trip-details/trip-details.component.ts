import { Component, OnInit } from '@angular/core';
import { Pagination } from '../_models/pagination';
import { TripParams } from '../_models/tripParams';
import { AtractieTuristica } from '../_models/atractieturistica';
import { Hotel } from '../_models/hotel';
import { ActivatedRoute, Router } from '@angular/router';
import { TripPlanningService } from '../_services/tripplanning.service';

@Component({
  selector: 'app-trip-details',
  templateUrl: './trip-details.component.html',
  styleUrls: ['./trip-details.component.css']
})
export class TripDetailsComponent implements OnInit {

  pagination: Pagination | undefined;
  tripParams: TripParams | undefined;
  atractiiTuristice: AtractieTuristica[] = [];
  hoteluri:Hotel[] = [];
  originCity: string = '';
  destinationCity: string = '';
  originLat = 37.7749; // Set the origin latitude
  originLng = -122.4194; // Set the origin longitude
  destinationLat = 34.0522; // Set the destination latitude
  destinationLng = -118.2437; // Set the destination longitude
  origin: any;
  cityIdFromParams: number = 0;
  destination: any;
  public directionsResponse: any;
  displayForAddingNewHotel: boolean = false;
  newHotel: Hotel = {
    nume: '',
    adresa:'',
    pricePerNight: 0,
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
  constructor(private route: ActivatedRoute, 
    private router: Router, private tripPlanningService : TripPlanningService) { 
      this.origin = { lat: "48.85637149999999", lng: "2.3532147" };
      this.destination = { lat: "44.4265892", lng: "26.1027819" };
    }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.originCity = params['sourceCityName'];
      this.destinationCity = params['destinationCityName'];
      let cityId = params['destinationCityId'];
      if(cityId){
        this.cityIdFromParams = cityId;
        this.tripPlanningService.getAtractiiTuristiceByCityId(cityId).subscribe({
          next: response => {
            this.atractiiTuristice = response;
          }
        });
        this.tripPlanningService.getHoteluriByCityId(cityId).subscribe({
          next: response => {
            this.hoteluri = response;
          }
        });
      } 
      // if(this.originCity != '' && this.destinationCity != ''){
      // }
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
      next: () => {
        this.atractiiTuristice.push(this.newAtractie);
      },
      error: error => {
        this.validationErrors = error
      } 
    })
    // Clear the form fields
    this.newAtractie = {
      cityId: 0,
      nume:'',
      descriere:'',
      adresa:'',
    };
  }
  addHotel(){
    this.displayForAddingNewHotel = true;
  }
  submitHotel(){
    this.newHotel.cityId = this.cityIdFromParams;
    
    // Add the new review to the reviews array

    this.tripPlanningService.addNewHotel(this.newHotel).subscribe({
      next: () => {
        this.hoteluri.push(this.newHotel);
      },
      error: error => {
        this.validationErrors = error
      } 
    })
    // Clear the form fields
    this.newHotel = {
      nume: '',
      adresa:'',
      pricePerNight: 0,
      cityId:0,
    };
  }
}
