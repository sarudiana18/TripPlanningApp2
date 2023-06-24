import { Photo } from "./photo";
import { Review } from "./review";

export interface Hotel{
    nume: string;
    pricePerNight?: number;
    pricePerNightCameraTripla?: number;
    rating?: number;
    adresa:string;
    cityId: number;
    photoUrl?: string;
    photos?: Photo[];
    reviews?: Review[];
    id?:number;
}
export class HotelFilter{
    pageNumber: number = 1;
    pageSize: number = 5;
    sortOrder: number = 1;
    sortField: string = "nume";
    nume?: string;
    pricePerNight?: number;
    pricePerNightCameraTripla?: number;
    rating?: number;
    adresa?:string;
    cityId?: number;
    photoUrl?: string;
    photos?: Photo[];
    reviews?: Review[];
    id?:number;
    nrPersoane = 0;
    buget = 0;
    startDate = '';
    endDate = '';
    nrNopti = 0;
}