import { Country } from "./country";

export interface City {
  id: number;
  name: string;
  state_Id: number;
  state_Code: string;
  country_Id: number;
  country_Code: string;
  latitude: number;
  longitude: number;
  created_At: Date;
  updated_At: Date;
  flag: boolean;
  wikiDataId: string;
  country: Country;
}