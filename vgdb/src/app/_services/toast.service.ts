import { Injectable } from "@angular/core";

const defaultDuration = 2000;

@Injectable({
  providedIn: 'root'
})

export class ToastService {
  public success(msg: string, duration: number = defaultDuration) {
    const toastSuccess = document.getElementById('toast-success');
    const toastSuccessMsg = document.getElementById('toast-success-message');

    if (toastSuccessMsg) {
      toastSuccessMsg.innerHTML = msg;
    }

    toastSuccess?.classList.remove('hidden');

    setTimeout(() => {
      toastSuccess?.classList.add('hidden');
    }, duration);
  }

  public error(msg: string, duration: number = defaultDuration) {
    const toastError = document.getElementById('toast-error');
    const toastErrorMsg = document.getElementById('toast-error-message');

    if (toastErrorMsg) {
      toastErrorMsg.innerHTML = msg;
    }

    toastError?.classList.remove('hidden');

    setTimeout(() => {
      toastError?.classList.add('hidden');
    }, duration);
  }
}
