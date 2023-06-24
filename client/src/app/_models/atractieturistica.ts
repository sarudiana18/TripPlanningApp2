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
export class AtractieTuristicaFilter{
    nume?: string;
    descriere?: string;
    adresa?:string;
    cityId?: number;
    photoUrl?: string;
    photos?: Photo[];
    id?: number;
    pageNumber: number = 1;
    pageSize: number = 5;
    sortOrder: number = 1;
    sortField: string = "nume";
}