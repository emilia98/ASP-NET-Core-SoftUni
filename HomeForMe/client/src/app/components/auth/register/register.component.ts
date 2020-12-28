import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any = {}
  errors:any = []

  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  register(registerForm: NgForm) {
    this.errors = [];

    this.authService.register(this.model)
      .subscribe((response: any) => {
        console.log(response);
        if (response.hasSuccess) {
          this.toastrService.success(response.message);
        }
        this.router.navigateByUrl('/login');
        registerForm.reset();
      }, (error: any) => {
        console.log(error);
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
