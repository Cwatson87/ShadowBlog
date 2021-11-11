let index = 0

//Need AddTag, DeleteTag, Code for Submitting, Search
function AddTag() {
    let tagEntry = document.getElementById("tagEntry")

    // I will need to make sure that there is Tag Text before allowing it to be added
    let searchResult = serach(tagentry.value);
    if (searchResult != null) {
        alert(searchResult);
    }
    else {
        let newTag = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newTag;
    }
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    //Gotta make sure the user selected an option before allowing them to delete
    let tagCount = 1
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;


    if (tagList.selectedIndex == -1) {
        alert("A Tag must be selected before it can be deleted!...Mate");
        return true;
    }

    //Else try to delete the selected tag
    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;

            //--variable vs variable--
            //--variable usually menas decrement the variable before using it
            //variable-- usually means decrement the variable after using it

            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }
    return true;
}

function Search(searchStr) {
    //Make sure they gave me something in the Text Tag
    if (searchStr == "") {
        return "Empty Tags are not allowed!";
    }

    //Need to make sure the user is giving me a dupe tag value
    var tagSelect = document.getElementById("TagList")
    if (tagSelect) {
        let options = tagSelect.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == searchStr) {
                return "Crikey Mate! What are you doing!? (Duplicate Tags are not allowed)"
            }
        }
    }
}


//js Code to select all of my options during a form submission

document.getElementById("frmCreate").addEventListener("submit", function);
Array.from(document.getElementById("TagList").options).forEach(opt => opt.selected = "selected");
   //write the one liner that selects all the options so they can be forwarded to the HttpPost
});