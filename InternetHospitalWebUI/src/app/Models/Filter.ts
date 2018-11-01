export class Filter {
    selectedSpecialization: string = "";
    searchKey: string;
    isWithParams: boolean;
  
    public CheckIfPropertyExist():void {
      if (this.searchKey || this.selectedSpecialization) {
        this.isWithParams = true;
      }
      else {
        this.isWithParams = false;
      }
    }
  }