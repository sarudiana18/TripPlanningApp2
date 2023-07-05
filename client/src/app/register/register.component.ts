import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';
import { City } from '../_models/city';
import { Country } from '../_models/country';
import { TripPlanningService } from '../_services/tripplanning.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;
  cities: City[] = [];
  countries: Country[] = [];

  constructor(private accountService: AccountService, private toastr: ToastrService, 
      private fb: FormBuilder, private router: Router, private tripPlanningService: TripPlanningService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
    this.loadCountries();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, 
        Validators.minLength(8), Validators.maxLength(16), Validators.pattern(/[A-Z]/), Validators.pattern(/[1-9]/)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {notMatching: true}
    }
  }

  register() {
    const dob = this.getDateOnly(this.registerForm.controls['dateOfBirth'].value);
    const values = {...this.registerForm.value, dateOfBirth: dob};
    this.accountService.register(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/planYourTrip')
      },
      error: error => {
        var error1 = error.error[0].description;
        this.validationErrors?.push(error1);
      } 
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    let theDob = new Date(dob);
    return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset()))
      .toISOString().slice(0,10);
  }
  loadCities() { 
    if(this.registerForm.controls['country'].value){
      this.tripPlanningService.getCitiesByCountryId(this.registerForm.controls['country'].value.id).subscribe({
        next: response => {
          this.cities = response;
        }
      })
    }
  }

  loadCountries() {
      this.tripPlanningService.getCountries().subscribe({
        next: response => {
          this.countries = response;
        }
      })
  }

}
