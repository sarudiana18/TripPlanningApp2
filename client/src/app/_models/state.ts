import { City } from "./city";
import { Country } from "./country";

export interface State {
  id: number;
  name: string;
  country_Id: number;
  country_Code: string;
  fips_Code: string;
  iso2: string;
  type: string;
  latitude: number | null;
  longitude: number | null;
  created_At: Date | null;
  updated_At: Date;
  flag: boolean;
  wikiDataId: string;
  cities: City[];
  country: Country;
}