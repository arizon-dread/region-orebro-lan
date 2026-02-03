import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import localeSe from '@angular/common/locales/sv';
import { registerLocaleData } from '@angular/common';

registerLocaleData(localeSe, 'sv');
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(),
    {provide: LOCALE_ID, useValue: 'sv'}
  ]
};
