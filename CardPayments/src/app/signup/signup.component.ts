import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styles: [
  ]
})
export class SignupComponent implements OnInit {

  public signupForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.signupForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', Validators.required],
      mobile: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  registerUser() {
    this.httpClient.post<any>("http://localhost:3000/registerUsers", this.signupForm.value)
      .subscribe(res => {
        alert("Signup successfully!");
        this.signupForm.reset();
        this.router.navigate(['login']);
      }, err => {
        alert("Something went wrong!");
      })
  }

}
