import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/services/admin/location.service';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.css']
})
export class LocationsComponent implements OnInit {
  locations: any = []

  constructor(
    private locationService: LocationService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.locationService.getAll()
      .subscribe((response: any) => {
        this.locations = response.locations;
        console.log(this.locations)
      }, (error: any) => {
        if (error.status == 401) {
          this.toastrService.error('Cannot access this page!')
          this.router.navigateByUrl('/');
        }
        console.log(error);
      });
  }
  
  deleteLocation(id : any) {
    this.locationService.delete(id)
    .subscribe((response: any) => {
      if (response.hasSuccess) {
        this.toastrService.success(response.message);

        this.locations = this.locations.map(l => {
          if (l.id == id) {
            return response.location
          }

          return l
        })
      }
    }, (error: any) => {
      if (error.status == 401) {
        this.toastrService.error('Cannot access this page!')
        this.router.navigateByUrl('/');
      } else if (error.status == 400) {
        this.toastrService.error('An error occurred while processing this actions!')
      }
      
      console.log(error);
    });
  }
}
