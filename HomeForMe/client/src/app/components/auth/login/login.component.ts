import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  errors: any = [];

  constructor(
    private authService: AuthService, 
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  login(loginForm: NgForm) {
    this.errors = [];

    this.authService.login(this.model)
      .subscribe((response : any) => {
        if (response.hasSuccess) {
          this.toastrService.success(response.message, 'Authentication successful!')
        }
        this.router.navigateByUrl('/');
        loginForm.reset()
      }, (error : any) => {
        if (error.error.hasError) {
          this.toastrService.error(error.error.message)
        } else if (error.error.errors) {
          this.errors = error.error.errors;
          this.toastrService.error("Fill up the form properly to log in!")
        } else {
          this.toastrService.error(error.message ?? error)
        }
      })
  }
}