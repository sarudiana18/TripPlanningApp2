import { Photo } from "./photo";
import { Review } from "./review";

export interface Parc{
    nume: string;
    rating?: number;
    adresa:string;
    cityId: number;
    photoUrl?: string;
    photos?: Photo[];
    reviews?: Review[];
    id?:number;
}
export class ParcFilter{
    nume?: string;
    rating?: number;
    adresa?:string;
    cityId?: number;
    photoUrl?: string;
    photos?: Photo[];
    reviews?: Review[];
    id?:number;
    pageNumber: number = 1;
    pageSize: number = 5;
    sortOrder: number = 1;
    sortField: string = "nume";
}