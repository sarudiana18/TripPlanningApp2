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