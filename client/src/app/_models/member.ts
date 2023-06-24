import { City } from "./city";
import { Country } from "./country";
import { Photo } from "./photo";
import { State } from "./state";

export interface Member {
    id: number;
    userName: string;
    photoUrl: string;
    age: number;
    lastName: string;
    created: Date;
    lastActive: Date;
    firstName: string;
    cityNume: string;
    city:City;
    country: Country;
    state: State;
    countryNume: string;
    photos: Photo[];
}