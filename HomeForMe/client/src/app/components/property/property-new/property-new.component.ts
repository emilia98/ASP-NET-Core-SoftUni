import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PropertyService } from 'src/app/services/property.service';

@Component({
  selector: 'app-property-new',
  templateUrl: './property-new.component.html',
  styleUrls: ['./property-new.component.css']
})
export class PropertyNewComponent implements OnInit {
  model: any = {};
  errors: any = [];
  locations: any = [];
  types: any = [];

  constructor(
    private propertyService: PropertyService,
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  newProperty(propertyNew: NgForm) {
    this.errors = [];

    this.propertyService.addNew(this.model)
      .subscribe((response: any) => {
        if (response.hasSuccess) {
          this.toastrService.success(response.successMessage);
        }
        this.router.navigateByUrl('/properties/my');
        propertyNew.reset();
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

  getData() {
    this.propertyService.getAddData()
      .subscribe((response: any) => {
        this.locations = response.locations;
        this.types = response.propertyTypes;
      }, error => {
        this.toastrService.error(error?.error && "An error occurred while fetching resources from server!");
      })
  }
}