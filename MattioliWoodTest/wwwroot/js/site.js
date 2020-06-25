// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
	acc[i].addEventListener("click", function () {
		this.classList.toggle("active");
		var panel = this.nextElementSibling;
		if (panel.style.display === "block") {
			panel.style.display = "none";
		} else {
			panel.style.display = "block";
		}
	});
}

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
		addressForm.style.visibility = 'visible';
		addressForm.style.display = 'block';

		const addressLine1Input = document.getElementById('firstLineAddress');
		addressLine1Input.setAttribute("required", "");


		const postcodeInput = document.getElementById('Postcode');
		postcodeInput.setAttribute("required", "");
		
	} else {
		addressForm.style.visibility = 'hidden';
		addressForm.style.display = 'none';

		const addressLine1Input = document.getElementById('Address1');
		addressLine1Input.removeAttribute("required"); 
		const postcodeInput = document.getElementById('Postcode');
		postcodeInput.removeAttribute("required"); 

	
	}


}