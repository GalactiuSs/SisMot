// const modalRequestDetails = document.getElementById('modalRequestDetails');
// const txtMotelName = document.getElementById('txtMotelName');
// const txtMotelDescription = document.getElementById('txtMotelDescription');
// const txtMotelPrice = document.getElementById('txtMotelPrice');
// const txtMotelPhone = document.getElementById('txtMotelPhone');
// const txtMotelLatitude = document.getElementById('txtMotelLatitude');
// const  txtMotelLongitude = document.getElementById('txtMotelLongitude');
//
// var requestDetailModal;
//
// function showDetailsRequest(id){
//     fetch(`/Request/GetRequestByDetails?requestId=${id}`)
//     .then(res => res.json())
//     .then(data => {
//         if(data !== null){
//             console.log(data);
//             requestDetailModal = new bootstrap.Modal(modalRequestDetails);
//             txtMotelName.value = data.motelName;
//             txtMotelDescription.value = data.motelDescription;
//             txtMotelPrice.value = data.motelPrice;
//             txtMotelPhone.value = data.motelPhoneNumber;
//             txtMotelLatitude.value = data.motelLatitude;
//             txtMotelLongitude.value = data.motelLongitude;
//             requestDetailModal.show();
//         }
//     })
//     .catch(err => console.error(err))
// }
