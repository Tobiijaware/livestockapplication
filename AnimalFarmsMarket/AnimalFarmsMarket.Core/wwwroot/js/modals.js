$(document).ready(function () {

    //Replace dropdown text when dropdown item is clicked
    var selectedText = "";
    $(".dropdown-menu").on('click', '.dropdown-item', function (e) {
        e.preventDefault();
        selectedText = $(this).text();
        $(this).closest(".dropdown").find(".dropdown-text").text(selectedText);
        $("#location").val(selectedText);
        $("#livestockLocation").val(selectedText);
    });

    var selectedType = "";
    var newSelectedType = "";
    $(".dropdown-menu").on('click', '.selected-livestock-type', function (e) {

        newSelectedType = $(this).data("type");

        $('#type').val(selectedType);

        //fetch data for selected type if it has not already been selected
        if (selectedType != newSelectedType) {
            selectedType = newSelectedType;

            $('#type').val(selectedType);

            //empty dropdown items in preparation for filling with new items
            $('#breed-list').empty();
            $('#sex-list').empty();
            $('#weight-list').empty();

            //fetch data for selected type
            $.getJSON("/Market/GetDataForLivestockCategory", { livestockCategory: selectedType }, function (response) {
                var retrievedLivestock = JSON.parse(response);

                //if fetched data is empty indicate, else populate dropdowns with fetched data
                if (retrievedLivestock.data[0].length == 0) {
                    $('#breed-list').append(
                        '<li class="pl-3 py-2 dropdown-item selected-livestock-breed disabled">No breed found</li>'
                    );
                    $('#sex-list').append(
                        '<li class="pl-3 py-2 dropdown-item disabled">No sex found</li>'
                    );
                    $('#weight-list').append(
                        '<li class="pl-3 py-2 dropdown-item selected-livestock-weight disabled">No weight found</li>'
                    );

                    //disable search button
                    $("#searchLivestock").prop("disabled", true);
                }
                else {

                    //enable search button
                    $("#searchLivestock").removeAttr("disabled");

                    //append fetched breeds to breed dropdown
                    for (let i = 0, numOfBreeds = retrievedLivestock.data[0].length; i < numOfBreeds; i++) {
                        $('#breed-list').append(
                            '<li class="pl-3 py-2 dropdown-item selected-livestock-breed" data-breed="' + retrievedLivestock.data[0][i] + '">'
                            + retrievedLivestock.data[0][i] +
                            '</li>'
                        );
                    }

                    //append fetched sexes to sex dropdown
                    for (let j = 0, numberOfSexes = retrievedLivestock.data[1].length; j < numberOfSexes; j++) {
                        if (retrievedLivestock.data[1][j] == "1")
                            $('#sex-list').append(
                                '<li class="pl-3 py-2 dropdown-item selected-livestock-sex" data-sex="1">Male</li>'
                            );
                        else
                            $('#sex-list').append(
                                '<li class="pl-3 py-2 dropdown-item selected-livestock-sex" data-sex="Female">Female</li>'

                            );
                    }

                    //append fetched weights to weights dropdown
                    for (let k = 0, numberOfWeights = retrievedLivestock.data[2].length; k < numberOfWeights; k++) {
                        $('#weight-list').append(
                            '<li class="pl-3 py-2 dropdown-item selected-livestock-weight" data-weight="'
                            + retrievedLivestock.data[2][k] + '">'
                            + retrievedLivestock.data[2][k]
                            + 'Kg</li>'
                        );
                    }
                }

            });
        }
    });

    //assign selected breed to breed input element
    $(".dropdown-menu").on('click', '.selected-livestock-breed', function (e) {

        var livestockBreed = $(this).data("breed");
        $('#breed').val(livestockBreed);
    });

    //assign selected sex to sex input element
    $(".dropdown-menu").on('click', '.selected-livestock-sex', function (e) {

        var livestockSex = $(this).data("sex");
        $('#sex').val(livestockSex);
    });

    //assign selected weight to weight input element
    $(".dropdown-menu").on('click', '.selected-livestock-weight', function (e) {

        var livestockWeight = $(this).data("weight");
        $('#weight').val(livestockWeight);
    });

});