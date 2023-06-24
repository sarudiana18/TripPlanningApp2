import { Photo } from "./photo";
import { Review } from "./review";

export interface Restaurant{
    nume: string;
    rating?: number;
    adresa:string;
    cityId: number;
    photoUrl?: string;
    photos?: Photo[];
    reviews?: Review[];
    id?:number;
    specific?:string;
}
export class RestaurantFilter{
    nume?: string;
    rating?: number;
    adresa?:string;
    cityId?: number;
    photoUrl?: string;
    photos?: Photo[];
    reviews?: Review[];
    id?:number;
    specific?:string;
    pageNumber: number = 1;
    pageSize: number = 5;
    sortOrder: number = 1;
    sortField: string = "nume";
}