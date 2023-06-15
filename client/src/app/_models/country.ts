import { City } from "./city";
import { State } from "./state";


export interface Country {
    id: number;
    name: string;
    iso3:string;
    numeric_Code: string;
    iso2: string;
    phoneCode: string;
    capital: string;
    currency: string;
    currency_Name: string;
    currency_Symbol: string;
    tld: string;
    native: string;
    region: string;
    subregion: string;
    timezones: string;
    translations: string;
    latitude?: number;
    longitude?: number;
    emoji: string;
    emojiU: string;
    created_At?: Date;
    updated_At: Date;
    flag: boolean;
    wikiDataId: string;
    cities: City[];
    states: State[];
}