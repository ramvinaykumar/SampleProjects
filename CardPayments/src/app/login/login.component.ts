import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  login() {
    this.httpClient.get<any>("http://localhost:3000/registerUsers")
      .subscribe(res => {
        console.log(res);
        const user = res.find((u: any) => {
          return u.email === this.loginForm.value.email && u.password === this.loginForm.value.password
        });
        if (user) {
          alert("Login successfully!");
          this.loginForm.reset();
          this.router.navigate(['dashboard']);
        }
        else {
          alert("Something went wrong!");
        }
      })
  }

}
