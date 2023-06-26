import { ChangeDetectorRef, Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { City } from 'src/app/_models/city';
import { Country } from 'src/app/_models/country';
import { Member } from 'src/app/_models/member';
import { State } from 'src/app/_models/state';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/member.service';
import { TripPlanningService } from 'src/app/_services/tripplanning.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm | undefined;
  cities: City[] = [];
  countries: Country[] = [];
  states: State[] = [];
  showCities: boolean = false;

  locationChanged:boolean = false;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  member: Member | undefined;
  user: User | null = null;

  constructor(private accountService: AccountService, private memberService: MembersService,
    private toastr: ToastrService, private tripPlanningService: TripPlanningService, private cdref: ChangeDetectorRef) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    })
  }

  ngOnInit(): void {
    this.loadMember();
  }
  ngAfterContentChecked() {
    this.cdref.detectChanges();
  }

  loadMember() {
    if (!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member => {
        this.member = member;
        this.loadCountries();
        this.loadStates();
        this.loadCities();
      }
    })
  }

  updateMember() {
    if(this.member){
      this.memberService.updateMember(this.member).subscribe({
        next: _ => {
          this.toastr.success('Porfilul a fost actualizat cu succes');
          this.editForm?.reset(this.member);
          this.locationChanged=false;
        }
      })    
    }
  }
  locationHasChanged(){
    this.locationChanged=true;
  }
  loadCities() {
    if (this.member?.state) {
      this.tripPlanningService.getCities(this.member.state.id).subscribe({
        next: response => {
          this.cities = response;
          if (this.cities.length == 0 && this.member?.city) {
            this.showCities = false;
            if(this.locationChanged){
              this.member.city = {} as City
              this.member.city.name = this.member.state.name;
              this.member.city.id = this.member.state.id;
            }
          }
          else {
            this.showCities = true;
          }
        }
      })
    }
  }

  loadCountries() {
    this.tripPlanningService.getCountries().subscribe({
      next: response => {
        this.countries = response;
      }
    })
  }
  loadStates() {
    if (this.member?.country) {
      this.tripPlanningService.getStates(this.member?.country.id).subscribe({
        next: response => {
          this.states = response;
          if(this.locationChanged && this.member?.state){
            this.member.state = {} as State;
          }
        }
      })
    }
  }
  
}
