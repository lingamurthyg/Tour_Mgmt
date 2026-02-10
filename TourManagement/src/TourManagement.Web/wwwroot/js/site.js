// Site-specific JavaScript

// Preview image before upload
function previewImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').attr('src', e.target.result).show();
        };
        reader.readAsDataURL(input.files[0]);
    }
}

// Calculate total price for bookings
function calculateTotalPrice(pricePerPerson, numberOfPeople) {
    var total = pricePerPerson * numberOfPeople;
    $('#totalPrice').text('$' + total.toFixed(2));
}

// Confirm delete action
function confirmDelete(entityName) {
    return confirm('Are you sure you want to delete this ' + entityName + '?');
}
