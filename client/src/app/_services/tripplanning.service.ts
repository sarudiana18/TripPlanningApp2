import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { TripParams } from '../_models/tripParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { State } from '../_models/state';
import { Country } from '../_models/country';
import { City } from '../_models/city';
import { AtractieTuristica } from '../_models/atractieturistica';
import { Hotel } from '../_models/hotel';
import { Review } from '../_models/review';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class TripPlanningService {
  baseUrl = environment.apiUrl;
  directionAPIKey = environment.DIRECTION_API_KEY;
  city: City | undefined;
  tripParams: TripParams | undefined;
  axios = require('axios');

  constructor(private http: HttpClient, private accountService: AccountService) {
   
  }

  getTripParams() {
    return this.tripParams;
  }

  setTripParams(params: TripParams) {
    this.tripParams = params;
  }

  resetTripParams() {
    if (this.city) {
      this.tripParams = new TripParams();
      return this.tripParams;
    }
    return;
  }
  getCities(stateId: number) {
    return this.http.get<City[]>(this.baseUrl + 'cities/getCitiesByStateId/'+ stateId).pipe(
      map(response => {
        return response;
      })
    )
  }

  getCountries() {
    
    return this.http.get<Country[]>(this.baseUrl + 'countries').pipe(
      map(response => {
        return response;
      })
    )
  }

  getStates(countryId: number) {
    
    return this.http.get<State[]>(this.baseUrl + 'states/getStatesByCountryId/'+countryId).pipe(
      map(response => {
        return response;
      })
    )
  }

  getAtractieTuristicaById(id: number){
    return this.http.get<AtractieTuristica>(this.baseUrl + 'atractiiTuristice/getAtractieById/'+ id).pipe(
      map(response => {
        return response;
      })
    )
  }
  getAtractiiTuristiceByCityId(cityId: number){
    return this.http.get<AtractieTuristica[]>(this.baseUrl + 'atractiiTuristice/getAtractieByCityId/'+ cityId).pipe(
      map(response => {
        return response;
      })
    )
  }
  getHoteluriByCityId(cityId: number){
    return this.http.get<Hotel[]>(this.baseUrl + 'hoteluri/getHotelByCityId/'+cityId).pipe(
      map(response => {
        return response;
      })
    )
  }

  getAtractiiTuristiceByStateId(stateId: number){
    return this.http.get<AtractieTuristica[]>(this.baseUrl + 'atractiiTuristiceByStateId/'+stateId).pipe(
      map(response => {
        return response;
      })
    )
  }
  getHotelById(id: number){
    return this.http.get<Hotel>(this.baseUrl + 'hoteluri/getHotelById/'+ id).pipe(
      map(response => {
        return response;
      })
    )
  }
  getHoteluriByStateId(stateId: number){
    return this.http.get<Hotel[]>(this.baseUrl + 'hoteluriByStateId/'+stateId).pipe(
      map(response => {
        return response;
      })
    )
  }
  getReviewsByHotelId(hotelId: number){
    return this.http.get<Review[]>(this.baseUrl + 'review/'+hotelId).pipe(
      map(response => {
        return response;
      })
    )
  }

  getDirection(from: string, to:string){
    const url1 = 'https://maps.googleapis.com/maps/api/directions/json?origin='+from+'&destination='+to+'&key='+this.directionAPIKey;
    const config = {
      method: "get",
      url: url1,
      headers: {},
    };
    return new Observable<any>((observer) => {
      this.axios(config)
        .then(function (response: any): void {
          observer.next(response.data);
          // return response.data;
        })
        .catch(function (error: any): void {
          observer.next(error);
        });
    });
    // return this.http.get<any>('https://maps.googleapis.com/maps/api/directions/json?origin='+from+'&destination='+to+'&key='+this.directionAPIKey).subscribe(response => {
    //     return response;
    //   })
  }

  addNewHotel(model: any){
    return this.http.post<Hotel>(this.baseUrl + 'hoteluri/addHotel', model).pipe(
      map(hotel => {
        if (hotel) {
        }
      })
    )
  }

  addNewAtractieTuristica(model: any){
    return this.http.post<AtractieTuristica>(this.baseUrl + 'atractiiTuristice/addAtractieTuristica/', model).pipe(
      map(atractie => {
        if (atractie) {
        }
      })
    )
  }
}
