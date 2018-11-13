import { Component, OnInit } from '@angular/core';
import { UsersProfileService } from '../../../Services/users-profile.service';
import { ImageValidationService } from '../../../Services/image-validation.service';
import { NotificationService } from '../../../Services/notification.service';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { HOST_URL } from '../../../config';
import { IllnessHistory } from 'src/app/Models/Illness-history';
import { ICurrentUser } from '../../../Models/CurrentUser';
import { LocalStorageService } from '../../../Services/local-storage.service';

const TOKEN = 'currentUser';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.scss']
})
export class UsersProfileComponent implements OnInit {

  constructor(private imageService: UsersProfileService, private imageValidator: ImageValidationService,
    private notification: NotificationService, private doctorService: DoctorsService, private storage: LocalStorageService) {
  }
  firstName = 'Vasul';
  secondName = 'Pochtarenko';
  lastName = 'Ivanovich';
  today = new Date();
  date = this.today.getDate() + '/' + this.today.getMonth() + '/' + this.today.getFullYear();

  user: ICurrentUser;
  token = TOKEN;


  tempHistory: IllnessHistory[] = [
    { dateTime: this.date, doctorName: 'Aloha1', diagnosis: 'Cancer', symptoms: 'Feels bad', treatment: 'Drink tea' },
    { dateTime: this.date, doctorName: 'Aloha2', diagnosis: 'Cancer', symptoms: 'Feels bad', treatment: 'Drink tea' },
    { dateTime: this.date, doctorName: 'Aloha3', diagnosis: 'Cancer', symptoms: 'Feels bad', treatment: 'Drink tea' },
    { dateTime: this.date, doctorName: 'Aloha4', diagnosis: 'Cancer', symptoms: 'Feels bad', treatment: 'Drink tea' }
  ];

  tempText = 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Dolorum nulla harum architecto velit saepe cumque amet voluptas rem repellat dignissimos dicta, quasi a, recusandae, nesciunt dolores aperiam eius tempore ad.';

  defaultImage = '../../assets/img/default.png';
  fileAvatar: File = null;
  imageToShow = this.defaultImage;

  getImageFromService() {
    this.imageService.getImage().subscribe((data: any) => {
      this.imageToShow = HOST_URL + data.avatarURL;
    }
    );
  }

  ngOnInit() {
    this.getImageFromService();
    this.doctorService.getSpecializations();
  }

  getAvatar(files: FileList) {
    this.fileAvatar = files.item(0);
    const reader = new FileReader();
    reader.readAsDataURL(files.item(0));

    reader.onload = (event) => {
      if (this.imageValidator.isImageFile(event.target.result)) {
        this.imageService.updateAvatar(this.fileAvatar).subscribe((shit: any) => {
          this.imageToShow = event.target.result;

          this.user = JSON.parse(localStorage.getItem(this.token));

          this.imageService.getImage().subscribe((data: any) => {
            this.user.user_avatar = data.avatarURL;
            this.storage.setItem(this.token, JSON.stringify(this.user), data.avatarURL);
          });
        });
      } else {
        this.notification.error('Only image file is acceptable!');
      }
    };
  }
}
