// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const form = document.getElementsByTagName('form');
const UserType = document.getElementById('UserType');
const addressForm = document.getElementById('AddressForm');

UserType.addEventListener('change', e => {
	e.preventDefault();

	checkInputs();
});

function checkInputs() {
	// trim to remove the whitespaces
	const userTypeValue = UserType.value;


	if (userTypeValue === 'Client') {
		document.getElementById('form').append = '<div class="form-group"> <label for="exampleFormControlInput1" > Date Of Birth</label ><input type="email" class="form-control" id="exampleFormControlInput1" placeholder="Enter Date Of Birth" required></div>'
		addressForm.style.visibility = 'visible';
		addressForm.style.display = 'block';
		
	} else {
		addressForm.style.visibility = 'hidden';
		addressForm.style.display = 'none';
	}


}