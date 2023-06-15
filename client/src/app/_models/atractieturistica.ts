import { Photo } from "./photo";

export interface AtractieTuristica{
    nume: string;
    descriere: string;
    adresa:string;
    cityId: number;
    photoUrl?: string;
    photos?: Photo[];
    id?: number;
}