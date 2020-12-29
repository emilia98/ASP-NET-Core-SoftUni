import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/services/admin/location.service';

@Component({
  selector: 'app-new-location',
  templateUrl: './new-location.component.html',
  styleUrls: ['./new-location.component.css']
})
export class NewLocationComponent implements OnInit {
  model: any = {};
  errors: any = [];

  constructor(
    private locationService: LocationService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
  }

  newLocation(locationNew: NgForm) {
    this.errors = [];

    this.locationService.addNew(this.model)
      .subscribe((response: any) => {
        console.log(response);
        if (response.hasSuccess) {
          this.toastrService.success(response.message);
        }
        this.router.navigateByUrl('/admin/locations');
        locationNew.reset();
      }, (error: any) => {
        console.log(error);
        if (error.error.hasFormError) {
          this.toastrService.error(error.error.hasFormError)
        } else if (error.error.errors) {
          this.errors = error.error.errors;
          this.toastrService.error("Fill up the form properly to add a new property!");
        } else {
          this.toastrService.error(error?.message ?? error)
        }
      });
  }
}
