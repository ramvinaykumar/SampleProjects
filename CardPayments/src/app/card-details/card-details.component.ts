import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CardDetail } from '../shared/card-detail.module';
import { CardDetailService } from '../shared/card-detail.service';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styles: [
  ]
})
export class CardDetailsComponent implements OnInit {

  constructor(public apiService: CardDetailService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.apiService.refreshData();
  }

  populateSelectedData(selectedRecord: CardDetail) {
    this.apiService.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this record?')) {
      this.apiService.deleteCardDetail(id)
        .subscribe(
          res => {
            this.apiService.refreshData();
            this.toastr.error("Deleted successfully", 'Card Detail Register');
          },
          err => { console.log(err) }
        )
    }
  }

}
