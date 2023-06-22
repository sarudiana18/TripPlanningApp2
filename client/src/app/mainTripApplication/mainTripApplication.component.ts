import { Component, OnInit } from '@angular/core';
import { City } from 'src/app/_models/city';
import { TripParams } from 'src/app/_models/tripParams';
import { Country } from 'src/app/_models/country';
import { State } from 'src/app/_models/state';
import { ChangeDetectorRef } from '@angular/core';
import { TripPlanningService } from '../_services/tripplanning.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

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
  user: User | undefined;

  constructor(private accountService: AccountService, private tripPlanningService: TripPlanningService, private cdref: ChangeDetectorRef,
    private fb: FormBuilder, private router: Router) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    })
  }


  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() + 1);
    this.loadCountries();
  }

  initializeForm() {
    this.tripPlanningForm = this.fb.group({
      nrPersoane: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      buget: ['', Validators.required],
      destinationCity: [''],
      destinationCountry: ['', Validators.required],
      destinationState: ['', Validators.required],
      sourceCity: [''],
      sourceCountry: [''],
      sourceState:['']
    });
  }

  showDropdownForSourceLocation(event?: any) {
    if (event == 'true') {
      this.plecareDinOrasulCurent = true;
      this.tripPlanningForm.controls["sourceCountry"].setValidators([Validators.required]);
      this.tripPlanningForm.controls["sourceState"].setValidators([Validators.required]);
    }
    else {
      this.plecareDinOrasulCurent = false;
      this.tripPlanningForm.controls["sourceCountry"].removeValidators([Validators.required]);
      this.tripPlanningForm.controls["sourceState"].removeValidators([Validators.required]);
    }
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }
  loadCities() {
    if (this.tripPlanningForm.controls['destinationState'].value) {
      this.tripPlanningService.getCities(this.tripPlanningForm.controls['destinationState'].value.id).subscribe({
        next: response => {
          this.cities = response;
          if (this.cities.length == 0) {
            this.showCities = false;
            this.tripPlanningForm.controls["destinationCity"].removeValidators([Validators.required]);
            this.tripParams.destinationCity = {} as City;
            this.tripParams.destinationCity.name = this.tripPlanningForm.controls["destinationState"].value.name;
            this.tripParams.destinationCity.id = this.tripPlanningForm.controls["destinationState"].value.id;
            this.tripParams.destinationCity.longitude = this.tripPlanningForm.controls["destinationState"].value.longitude;
            this.tripParams.destinationCity.latitude = this.tripPlanningForm.controls["destinationState"].value.latitude;
          }
          else {
            this.showCities = true;
            this.tripPlanningForm.controls["destinationCity"].removeValidators([Validators.required]);
          }
        }
      })
    }
  }

  loadSourceCities() {
    if (this.tripPlanningForm.controls['sourceState'].value) {
      this.tripPlanningService.getCities(this.tripPlanningForm.controls['sourceState'].value.id).subscribe({
        next: response => {
          this.sourceCities = response;
          if (this.sourceCities.length == 0) {
            this.showSourceCities = false;
            this.tripPlanningForm.controls["sourceCity"].removeValidators([Validators.required]);
            this.tripParams.sourceCity = {} as City;
            this.tripParams.sourceCity.name = this.tripPlanningForm.controls["sourceState"].value.name;
            this.tripParams.sourceCity.id = this.tripPlanningForm.controls["sourceState"].value.id;
            this.tripParams.sourceCity.longitude = this.tripPlanningForm.controls["sourceState"].value.longitude;
            this.tripParams.sourceCity.latitude = this.tripPlanningForm.controls["sourceState"].value.latitude;
          }
          else {
            this.showSourceCities = true;
            this.tripPlanningForm.controls["sourceCity"].setValidators([Validators.required]);
          }
        }
      })
    }
  }

  loadCountries() {
    if (this.tripParams) {
      this.tripPlanningService.setTripParams(this.tripParams);
      this.tripPlanningService.getCountries().subscribe({
        next: response => {
          this.countries = response;
        }
      })
    }
  }

  loadStates() {
    if (this.tripPlanningForm.controls['destinationCountry'].value) {
      this.tripPlanningService.getStates(this.tripPlanningForm.controls['destinationCountry'].value.id).subscribe({
        next: response => {
          this.states = response;
        }
      })
    }
  }

  loadSourceStates() {
    if (this.tripPlanningForm.controls['sourceCountry'].value) {
      this.tripPlanningService.getStates(this.tripPlanningForm.controls['sourceCountry'].value.id).subscribe({
        next: response => {
          this.sourceStates = response;
        }
      })
    }
  }
  cancel() {
    this.sourceCities = [];
    this.cities = [];
    this.sourceStates = [];
    this.states = [];
    this.showCities = false;
    this.showSourceCities = false;
    this.plecareDinOrasulCurent = true;
    this.tripParams = new TripParams();
  }
  generateResponseForSelectedValues() {
    if (this.plecareDinOrasulCurent && this.user?.oras) {
      this.tripParams.sourceCity = this.user.oras;
    }
    else if(!this.tripParams.sourceCity || this.tripParams.sourceCity.id != this.tripPlanningForm.controls["sourceState"].value.id){
      this.tripParams.sourceCity = this.tripPlanningForm.controls['sourceCity'].value;
    }

    if(!this.tripParams.destinationCity || this.tripParams.destinationCity.id != this.tripPlanningForm.controls["destinationState"].value.id){
      this.tripParams.destinationCity = this.tripPlanningForm.controls['destinationCity'].value;
    }
    this.tripParams.endDate = this.tripPlanningForm.controls['endDate'].value;
    this.tripParams.startDate = this.tripPlanningForm.controls['startDate'].value;
    this.tripParams.buget = this.tripPlanningForm.controls['buget'].value;
    this.tripParams.nrPersoane = this.tripPlanningForm.controls['nrPersoane'].value;
    this.tripParams.nrNopti = this.getNumberOfNghtsBetween2Dates(this.tripParams.endDate, this.tripParams.startDate);

    if (this.tripParams.destinationCity.id) {
      this.router.navigateByUrl('/trip-details', { state: this.tripParams });
    }
  }

  private getNumberOfNghtsBetween2Dates(date_1: string, date_2: string) {

    const days = (date_1_dateformat: Date, date_2_dateformat: Date) => {
      let difference = date_1_dateformat.getTime() - date_2_dateformat.getTime();
      let TotalDays = Math.ceil(difference / (1000 * 3600 * 24));
      return TotalDays;
    }
    return days(new Date(date_1), new Date(date_2));
  }

}
