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
import { AtractieTuristica, AtractieTuristicaFilter } from '../_models/atractieturistica';
import { Hotel, HotelFilter } from '../_models/hotel';
import { Review } from '../_models/review';
import { Parc, ParcFilter } from '../_models/parc';
import { Restaurant, RestaurantFilter } from '../_models/restaurant';

@Injectable({
  providedIn: 'root'
})
export class TripPlanningService {
  baseUrl = environment.apiUrl;
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
  getCitiesByCountryId(countryId: number) {
    return this.http.get<City[]>(this.baseUrl + 'cities/getCitiesByCountryId/'+ countryId).pipe(
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
  getParcuriByCityId(cityId: number){
    return this.http.get<Parc[]>(this.baseUrl + 'parcuri/getParcByCityId/'+ cityId).pipe(
      map(response => {
        return response;
      })
    )
  }
  getRestauranteByCityId(cityId: number){
    return this.http.get<Restaurant[]>(this.baseUrl + 'restaurante/getRestaurantByCityId/'+ cityId).pipe(
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
  getHoteluriByCityId(cityId: number){
    return this.http.get<Hotel[]>(this.baseUrl + 'hoteluri/getHotelByCityId/'+cityId).pipe(
      map(response => {
        return response;
      })
    )
  }
  getHoteluriByCityIdAndByBugetAndByNoOfNightsAndByNrPersoane(cityId: number, buget:number, numarNopti:number,
    nrPersoane: number){
    return this.http.get<Hotel[]>(this.baseUrl + 'hoteluri/getHotelByCityIdAndBugetAndNrNoptiAndNrPersoane/'
    +cityId+'/'+buget+'/'+numarNopti+'/'+nrPersoane).pipe(
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
  
  addNewHotel(model: any){
    return this.http.post<Hotel>(this.baseUrl + 'hoteluri/addHotel', model).pipe(
      map(hotel => {
        return hotel;
      })
    )
  }
  getReviewsByHotelId(hotelId: number){
    return this.http.get<Review[]>(this.baseUrl + 'review/getByHotelId/'+hotelId).pipe(
      map(response => {
        return response;
      })
    )
  }
  getParcById(id: number){
    return this.http.get<Parc>(this.baseUrl + 'parcuri/getParcById/'+ id).pipe(
      map(response => {
        return response;
      })
    )
  }
  getRestaurantById(id: number){
    return this.http.get<Restaurant>(this.baseUrl + 'restaurante/getRestaurantById/'+ id).pipe(
      map(response => {
        return response;
      })
    )
  }

 
  getReviewsByParcId(id: number){
    return this.http.get<Review[]>(this.baseUrl + 'review/getByParcId/'+id).pipe(
      map(response => {
        return response;
      })
    )
  }
  getReviewsByRestaurantId(id: number){
    return this.http.get<Review[]>(this.baseUrl + 'review/getByRestaurantId/'+id).pipe(
      map(response => {
        return response;
      })
    )
  }
  addNewRecenzie(model: any){
    return this.http.post<Review>(this.baseUrl + 'review/addReview', model).pipe(
      map(response => {
        return response;
      })
    )
  }

  addNewRestaurant(model: any){
    return this.http.post<Restaurant>(this.baseUrl + 'restaurante/addRestaurant', model).pipe(
      map(response => {
        return response;
      })
    )
  }
  addNewParc(model: any){
    return this.http.post<Parc>(this.baseUrl + 'parcuri/addParc', model).pipe(
      map(response => {
        return response;
      })
    )
  }

  addNewAtractieTuristica(model: any){
    return this.http.post<AtractieTuristica>(this.baseUrl + 'atractiiTuristice/addAtractieTuristica/', model).pipe(
      map(atractie => {
        return atractie
      })
    )
  }
  setMainPhoto(photoId: number, controllerName: string) {
    return this.http.put(this.baseUrl + controllerName+ '/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number, controllerName: string) {
    return this.http.delete(this.baseUrl + controllerName +'/delete-photo/' + photoId);
  }

  getHotels(hotelParams: HotelFilter) {


    let params = getPaginationHeaders(hotelParams.pageNumber, hotelParams.pageSize);

    params = params.append('nume', hotelParams.nume?? '');
    params = params.append('rating', hotelParams.rating?? '');
    params = params.append('adresa', hotelParams.adresa?? '');
    params = params.append('sortField', hotelParams.sortField?? '');
    params = params.append('cityId', hotelParams.cityId?? 0);
    params = params.append('buget', hotelParams.buget?? '');
    params = params.append('nrNopti', hotelParams.nrNopti?? '');
    params = params.append('nrPersoane', hotelParams.nrPersoane?? '');


    return getPaginatedResult<Hotel[]>(this.baseUrl + 'hoteluri', params, this.http).pipe(
      map(response => {
        return response;
      })
    )
  }
  getAtractiiTuristice(atractieParams: AtractieTuristicaFilter) {


    let params = getPaginationHeaders(atractieParams.pageNumber, atractieParams.pageSize);

    params = params.append('nume', atractieParams.nume?? '');
    params = params.append('adresa', atractieParams.adresa?? '');
    params = params.append('sortField', atractieParams.sortField?? '');
    params = params.append('cityId', atractieParams.cityId?? 0);


    return getPaginatedResult<AtractieTuristica[]>(this.baseUrl + 'atractiiTuristice', params, this.http).pipe(
      map(response => {
        return response;
      })
    )
  }

  getParcuri(parcParams: ParcFilter) {


    let params = getPaginationHeaders(parcParams.pageNumber, parcParams.pageSize);

    params = params.append('nume', parcParams.nume?? '');
    params = params.append('rating', parcParams.rating?? '');
    params = params.append('sortField', parcParams.sortField?? '');
    params = params.append('cityId', parcParams.cityId?? 0);
    params = params.append('adresa', parcParams.adresa?? '');


    return getPaginatedResult<Parc[]>(this.baseUrl + 'parcuri', params, this.http).pipe(
      map(response => {
        return response;
      })
    )
  }

  getRestaurante(restaurantParams: RestaurantFilter) {
    let params = getPaginationHeaders(restaurantParams.pageNumber, restaurantParams.pageSize);

    params = params.append('nume', restaurantParams.nume?? '');
    params = params.append('rating', restaurantParams.rating?? '');
    params = params.append('sortField', restaurantParams.sortField?? '');
    params = params.append('cityId', restaurantParams.cityId?? 0);
    params = params.append('adresa', restaurantParams.adresa?? '');
    params = params.append('specific', restaurantParams.specific?? '');

    return getPaginatedResult<Restaurant[]>(this.baseUrl + 'restaurante', params, this.http).pipe(
      map(response => {
        return response;
      })
    )
  }
  
  removeObject(path:string, objectId: number){
    return this.http.delete(this.baseUrl + path +'/' + objectId);
  }
}
