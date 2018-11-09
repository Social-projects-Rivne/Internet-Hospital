import { Component, OnInit } from '@angular/core';
import { DoctorDetails } from '../../Models/doctor-details';
import { DoctorService } from '../../Services/doctor.service';
import { ActivatedRoute} from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-page',
  templateUrl: './doctor-page.component.html',
  styleUrls: ['./doctor-page.component.scss']
})
export class DoctorPageComponent implements OnInit {

  doctor: DoctorDetails;
  tempImgs = [
    'https://pmcvariety.files.wordpress.com/2018/09/d-trump.jpg?w=1000',
    'https://ichef.bbci.co.uk/images/ic/720x405/p06f54c3.jpg',
    'https://s.abcnews.com/images/Politics/president-trump-remarks-gty-jef-181101_hpEmbed_2_3x2_992.jpg',
    'https://amp.businessinsider.com/images/5ab2798bb0284719008b4612-750-562.jpg',
    'https://i.cbc.ca/1.4805946.1535708704!/cpImage/httpImage/image.jpg_gen/derivatives/16x9_780/trump.jpg',
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_rSslFmeZyHhREKYVNBULCPwx6xC3JJeoV7MJmr0lHS3Wf0mKjw',
    'https://cdn.cnn.com/cnnnext/dam/assets/160118134132-donald-trump-nigel-parry-large-169.jpg',
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT6vbttT9OD7HiFrY4Fe53KjCE5JpOsqTr2ddUvNiYfbw7wVmie',
    'https://e3.365dm.com/18/08/1096x616/skynews-donald-trump-us-president_4407328.jpg?20180831214956',
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxPoDovI6jqh_ibsuW_SqhI2GoesEQrITQUc1OJDwb9o9zXJNR',
    'https://amp.businessinsider.com/images/5b84699464dce81e008b55e2-750-375.jpg'
  ];
  constructor(private activateRoute: ActivatedRoute, private service: DoctorService, private router: Router) { }

  ngOnInit() {
    const id = this.activateRoute.snapshot.params['id'];
    this.service.getDoctor(id).subscribe((data: any) => {
        this.doctor = data;
        console.log(data);
        console.log(this.doctor);
      });
  }

}
