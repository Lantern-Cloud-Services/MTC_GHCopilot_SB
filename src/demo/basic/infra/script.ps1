#Prompt:  create a bash script to rename files in a directory with the creation date appended to the filename
#!/bin/bash

# Directory containing the files
DIR="/path/to/directory"

# Loop through all files in the directory
for FILE in "$DIR"/*; do
    # Get the creation date of the file
    CREATION_DATE=$(stat -c %y "$FILE" | cut -d ' ' -f 1)
    
    # Get the file extension
    EXT="${FILE##*.}"
    
    # Get the file name without the extension
    FILENAME="${FILE%.*}"
    
    # Rename the file with the creation date appended
    mv "$FILE" "${FILENAME}_${CREATION_DATE}.${EXT}"
done