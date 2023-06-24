import { City } from "./city";
import { Country } from "./country";

export interface User {
    username: string;
    token: string;
    photoUrl: string;
    firstName: string;
    lastName: string;
    roles: string[];
    
    numeOrasCurent: string;
    oras: City;
    countryNume: string;
    country: Country
    id: number;
}