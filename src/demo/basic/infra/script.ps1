#Prompt:  create a bash script to rename files in a directory with the creation date appended to the filename
#!/bin/bash

# Directory containing the files
DIR="/path/to/directory"

# Loop through each file in the directory
for FILE in "$DIR"/*; do
    # Get the creation date of the file
    CREATION_DATE=$(stat -c %y "$FILE" | cut -d ' ' -f 1)
    
    # Get the file extension
    EXT="${FILE##*.}"
    
    # Get the file name without the extension
    FILENAME=$(basename "$FILE" ."$EXT")
    
    # Create the new file name with the creation date appended
    NEW_FILENAME="${FILENAME}_${CREATION_DATE}.${EXT}"
    
    # Rename the file
    mv "$FILE" "$DIR/$NEW_FILENAME"
done

