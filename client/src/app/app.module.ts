import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { RadioButtonModule } from 'primeng/radiobutton';
import { TabViewModule } from 'primeng/tabview';
import { DialogModule } from 'primeng/dialog';
import { RatingModule } from 'primeng/rating';
import { MainTripApplicationComponent } from './mainTripApplication/mainTripApplication.component';
import { TripDetailsComponent } from './trip-details/trip-details.component';
import { AtractieTuristicaComponent } from './atractie-turistica/atractie-turistica.component';
import { HotelComponent } from './hotel/hotel.component';
import { HotelDetailsComponent } from './hotel/hotel-details/hotel-details.component';
import { AtractieTuristicaDetailsComponent } from './atractie-turistica/atractie-turistica-details/atractie-turistica-details.component';
import { HotelReviewsComponent } from './hotel/hotel-reviews/hotel-reviews.component';
import { AgmCoreModule } from '@agm/core';
import { AgmDirectionModule } from 'agm-direction' 
import { ParcDetailsComponent } from './parc/parc-details/parc-details.component';
import { ParcReviewsComponent } from './parc/parc-reviews/parc-reviews.component';
import { RestaurantComponent } from './restaurant/restaurant.component';
import { ParcComponent } from './parc/parc.component';
import { RestaurantDetailsComponent } from './restaurant/restaurant-details/restaurant-details.component';
import { RestaurantReviewsComponent } from './restaurant/restaurant-reviews/restaurant-reviews.component';
import { PhotoEditorComponentAtractieTuristica } from './atractie-turistica/photo-editor/photo-editor.component';
import { PhotoEditorComponentRestaurant } from './restaurant/photo-editor/photo-editor.component';
import { PhotoEditorComponentHotel } from './hotel/photo-editor/photo-editor.component';
import { PhotoEditorComponentParc } from './parc/photo-editor/photo-editor.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberDetailComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DatePickerComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    PhotoManagementComponent,
    RolesModalComponent,
    ConfirmDialogComponent,
    MainTripApplicationComponent,
    TripDetailsComponent,
    AtractieTuristicaComponent,
    HotelComponent,
    HotelDetailsComponent,
    AtractieTuristicaDetailsComponent,
    HotelReviewsComponent,
    ParcComponent,
    ParcDetailsComponent,
    ParcReviewsComponent,
    RestaurantComponent,
    RestaurantDetailsComponent,
    RestaurantReviewsComponent,
    PhotoEditorComponentAtractieTuristica,
    PhotoEditorComponentRestaurant,
    PhotoEditorComponentHotel,
    PhotoEditorComponentParc
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    DropdownModule,
    CheckboxModule,
    ToggleButtonModule,
    RadioButtonModule,
    TabViewModule,
    DialogModule,
    RatingModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyB2qdNY_0dgdLFKb3jASFtiDjwPnd2R-Ug',
    }),
    AgmDirectionModule,
    SharedModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
