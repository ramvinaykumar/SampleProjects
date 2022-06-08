import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styles: [
  ]
})
export class UserDetailComponent implements OnInit {

  public userDetailForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.userDetailForm = this.formBuilder.group({
      fullName: [''],
      email: [''],
      mobile: ['']
    })

    
  }

  updateUser() {

  }

  fillFormData(selectedRecord: any) {
    console.log(selectedRecord);
    this.userDetailForm = Object.assign({}, selectedRecord);
  }

}
