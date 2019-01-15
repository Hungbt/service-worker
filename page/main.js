console.log("hello");
const applicationServerPublicKey = 'BM19ABrxlF90WXluoBYTPuOf53JzbgXKVaiMJAVo6HELcb01UfG6h7_Y0GYhJzVcEYWLirOwvCuUTY0dGCEnOgU';
let isSubscribed = false;
let swRegistration = null;
if ('serviceWorker' in navigator && 'PushManager' in window) {
  console.log('Service Worker and Push is supported');
  navigator.serviceWorker.register('sw.js')
    .then(function (swReg) {
      console.log('Service Worker is registered', swReg);
      swRegistration = swReg;
      initializeUI();
    })
    .catch(function (error) {
      console.error('Service Worker Error', error);
    });
} else {
  console.warn('Push messaging is not supported');
}

function initializeUI() {
  swRegistration.pushManager.getSubscription()
    .then(function (subscription) {
      isSubscribed = !(subscription === null);
      if (isSubscribed) {
        console.log(JSON.stringify(subscription));
      } else {
        subscribeUser();
      }
    });
}

function subscribeUser() {
  const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
  swRegistration.pushManager.subscribe({
      userVisibleOnly: true,
      applicationServerKey: applicationServerKey
    })
    .then(function (subscription) {
      console.log(JSON.stringify(subscription));
      isSubscribed = true;
    })
    .catch(function (err) {
      console.log('Failed to subscribe the user: ', err);
    });
}

function urlB64ToUint8Array(base64String) {
  const padding = '='.repeat((4 - base64String.length % 4) % 4);
  const base64 = (base64String + padding)
    .replace(/\-/g, '+').replace(/_/g, '/');
  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);
  for (let i = 0; i < rawData.length; ++i)
    outputArray[i] = rawData.charCodeAt(i);
  return outputArray;
}