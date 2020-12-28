import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PropertyService } from 'src/app/services/property.service';

@Component({
  selector: 'app-property-list-all',
  templateUrl: './property-list-all.component.html',
  styleUrls: ['./property-list-all.component.css']
})
export class PropertyListAllComponent implements OnInit {
  properties: any = [];
  defaultImage: string = 'https://www.tibs.org.tw/images/default.jpg';

  constructor(
    private propertyService: PropertyService,
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.propertyService.getAll()
      .subscribe((response: any) => {
        this.properties = response;
      }, (error: any) => {
        if (error.status == 401) {
          this.toastrService.error('Cannot access this page!')
          this.router.navigateByUrl('/');
        }
        console.log(error);
      });
  }
}
