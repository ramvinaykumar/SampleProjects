import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CardDetail } from 'src/app/shared/card-detail.module';
import { CardDetailService } from 'src/app/shared/card-detail.service';

@Component({
  selector: 'app-card-detail-form',
  templateUrl: './card-detail-form.component.html',
  styles: [
  ]
})
export class CardDetailFormComponent implements OnInit {

  constructor(public apiService: CardDetailService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.apiService.formData.cardDetailId == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.apiService.postCardDetail().subscribe(
      res => {
        this.resetForm(form);
        this.apiService.refreshData();
        this.toastr.success('Submitted successfully', 'Card Detail Register')
      },
      err => { console.log(err); }
    );
  }

  updateRecord(form: NgForm) {
    this.apiService.putCardDetail().subscribe(
      res => {
        this.resetForm(form);
        this.apiService.refreshData();
        this.toastr.info('Updated successfully', 'Card Detail Register')
      },
      err => { console.log(err); }
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.apiService.formData = new CardDetail();
  }

}
