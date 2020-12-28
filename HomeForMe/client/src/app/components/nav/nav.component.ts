import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  collapsed = true;
  
  constructor(
    public authService: AuthService, 
    private router: Router, 
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void {
  }

  logout() {
    this.authService.logout();
    this.toastrService.success('Successfully logged out!')
    this.router.navigateByUrl('/');
  }
}
