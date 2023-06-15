import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/_models/city';
import { TripParams } from 'src/app/_models/tripParams';
import { Country } from 'src/app/_models/country';
import { State } from 'src/app/_models/state';
import { ChangeDetectorRef } from '@angular/core';
import { TripPlanningService } from '../_services/tripplanning.service';
import { AtractieTuristica } from '../_models/atractieturistica';
import { Hotel } from '../_models/hotel';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainTripApplication',
  templateUrl: './mainTripApplication.component.html',
  styleUrls: ['./mainTripApplication.component.css']
})
export class MainTripApplicationComponent implements OnInit {
  
  cities: City[] = [];
  countries: Country[] = [];
  states: State[] = [];

  sourceCities: City[] = [];

  sourceStates: State[] = [];
  tripParams: TripParams = new TripParams();

  plecareDinOrasulCurent: boolean = true;
  showHotelsAndAttractions: boolean = false;
  showCities: boolean = false;
  showSourceCities: boolean = false;

  tripPlanningForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;

  constructor(private tripPlanningService : TripPlanningService, private cdref: ChangeDetectorRef,
    private fb: FormBuilder, private router: Router) {
  }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear()+1);
    this.loadCountries();
  }

  initializeForm() {
    this.tripPlanningForm = this.fb.group({
      nrPersoane: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      buget: ['', Validators.required]
      // destinationCity: ['', Validators.required],
      // destinationCountry: ['', Validators.required],
      // destinationState:['', Validators.required],
      // sourceCity: ['', Validators.required],
      // sourceCountry: ['', Validators.required],
      // sourceState:['', Validators.required]
    });
  }

  showDropdownForSourceLocation(event?: any){
    if(event=='true'){
      this.plecareDinOrasulCurent = true;
    }
    else{
      this.plecareDinOrasulCurent = false;
    }
    // var boolean = !!event;
    // console.log(typeof(boolean));
    // console.log(boolean);
    // //this.plecareDinOrasulCurent =  Boolean(this.plecareDinOrasulCurent);
    // console.log(this.plecareDinOrasulCurent);
    // console.log(this.plecareDinOrasulCurent.valueOf(), typeof(this.plecareDinOrasulCurent));
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
 }
  loadCities() {
    if (this.tripParams?.destinationState) {
      this.tripPlanningService.setTripParams(this.tripParams);
      this.tripPlanningService.getCities(this.tripParams.destinationState.id).subscribe({
        next: response => {
          this.cities = response;
          if(this.cities.length == 0){
            this.showCities = false;
            this.tripParams.destinationCity = {} as City;
            this.tripParams.destinationCity.name = this.tripParams.destinationState.name;
            this.tripParams.destinationCity.id = this.tripParams.destinationState.id;
          }
          else{
            this.showCities = true;
          }
        }
      })
    }
  }

  loadSourceCities() {
    if (this.tripParams?.sourceState) {
      this.tripPlanningService.setTripParams(this.tripParams);
      this.tripPlanningService.getCities(this.tripParams.sourceState.id).subscribe({
        next: response => {
          this.sourceCities = response;
          if(this.sourceCities.length == 0){
            this.showSourceCities = false;
            this.tripParams.sourceCity = {} as City;
            this.tripParams.sourceCity.name = this.tripParams.sourceState.name;
            this.tripParams.sourceCity.id = this.tripParams.sourceState.id;
          }
          else{
            this.showSourceCities = true;
          }
        }
      })
    }
  }

  loadCountries() {
    if(this.tripParams){
      this.tripPlanningService.setTripParams(this.tripParams);
      this.tripPlanningService.getCountries().subscribe({
        next: response => {
          this.countries = response;
        }
      })
    } 
  }

  loadStates() {
    if (this.tripParams?.destinationCountry) {
      this.tripPlanningService.setTripParams(this.tripParams);
      this.tripPlanningService.getStates(this.tripParams.destinationCountry.id).subscribe({
        next: response => {
          this.states = response;
        }
      })
    }
  }

  loadSourceStates() {
    if (this.tripParams?.sourceCountry) {
      this.tripPlanningService.setTripParams(this.tripParams);
      this.tripPlanningService.getStates(this.tripParams.sourceCountry.id).subscribe({
        next: response => {
          this.sourceStates = response;
        }
      })
    }
  }
  cancel() {
    this.sourceCities=[];
    this.cities = [];
    this.sourceStates = [];
    this.states = [];
    this.showCities = false;
    this.showSourceCities = false;
    this.plecareDinOrasulCurent = true;
    this.tripParams = new TripParams();
  }
  generateResponseForSelectedValues(){
    if(this.tripParams.destinationCity.id){
      this.router.navigateByUrl('/trip-details/'+ this.tripParams.sourceCity.name+'/'+this.tripParams.destinationCity.name +'/'+this.tripParams.destinationCity.id);
    }
  }
}
