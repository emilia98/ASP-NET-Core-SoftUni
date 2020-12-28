import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PropertyService } from 'src/app/services/property.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  properties: any = [];

  constructor(
    private propertyService : PropertyService,
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void {
    this.propertyService.getAll()
      .subscribe((response: any) => {
        this.properties = response.properties;
      }, error => {
        this.toastrService.error("Error occurred while fetching all properties");
      });
  }
}
