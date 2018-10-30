import { AdminpanelModule } from './adminpanel.module';

describe('AdminpanelModule', () => {
  let adminpanelModule: AdminpanelModule;

  beforeEach(() => {
    adminpanelModule = new AdminpanelModule();
  });

  it('should create an instance', () => {
    expect(adminpanelModule).toBeTruthy();
  });
});
