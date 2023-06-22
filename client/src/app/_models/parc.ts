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