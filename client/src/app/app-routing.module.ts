import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { MainTripApplicationComponent } from './mainTripApplication/mainTripApplication.component';
import { TripDetailsComponent } from './trip-details/trip-details.component';
import { AtractieTuristicaDetailsComponent } from './atractie-turistica/atractie-turistica-details/atractie-turistica-details.component';
import { HotelDetailsComponent } from './hotel/hotel-details/hotel-details.component';
import { RestaurantDetailsComponent } from './restaurant/restaurant-details/restaurant-details.component';
import { ParcDetailsComponent } from './parc/parc-details/parc-details.component';
import { TripObjectListComponent } from './admin/trip-objects-list/trip-objects-list.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '', 
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'planYourTrip', component: MainTripApplicationComponent},
      {path: 'trip-details', component: TripDetailsComponent},
      {path: 'atractiiTuristice/:atractieId', component: AtractieTuristicaDetailsComponent},
      {path: 'hoteluri/:hotelId/:startDate/:endDate', component: HotelDetailsComponent},
      {path: 'hoteluri/:hotelId', component: HotelDetailsComponent},
      {path: 'restaurante/:restaurantId', component: RestaurantDetailsComponent},
      {path: 'parcuri/:parcId', component: ParcDetailsComponent},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard]},
      {path: 'tripObjectsList', component: TripObjectListComponent},
      
    ]
  },
  {path: 'errors', component: TestErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
