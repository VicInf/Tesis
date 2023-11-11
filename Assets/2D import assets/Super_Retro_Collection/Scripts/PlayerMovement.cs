// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class PlayerMovement : MonoBehaviour
{
    // static public members
    public static PlayerMovement instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    //public Animator animator;
    //public SpriteRenderer spriteRenderer;

    // -----------------------------------------------------------------------------------------
    // private members
    public Vector2 movement;

    // The name of the sprite sheet to use
    //public string SpriteSheetName;

    // The name of the currently loaded sprite sheet
    //private string LoadedSpriteSheetName;

    // The dictionary containing all the sliced up sprites in the sprite sheet
   // private Dictionary<string, Sprite> spriteSheet;

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;
        //this.LoadSpriteSheet();
        //animator.SetFloat("speed", 0);
        //animator.SetInteger("orientation", 4);
    }
    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // update members
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //animationUpdate();
    }

    // Runs after the animation has done its work
    private void LateUpdate()
    {
        /*// Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            // Load the new sprite sheet
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        // Important: The name of the sprite must be the same!
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];*/
    }

    // -----------------------------------------------------------------------------------------
    // Set the animation parameters
   /* public void animationUpdate()
    {
        if (movement.x > 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;


        animator.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        //animator.SetInteger("orientation", 0);
        if (movement.x != 0)
            animator.SetInteger("orientation", 2);
        if (movement.y == 1)
            animator.SetInteger("orientation", 0);
        if (movement.y == -1)
            animator.SetInteger("orientation", 4);
    }
    // -----------------------------------------------------------------------------------------
    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        string spritesheetfolder = "Characters/";
        string spritesheetfilepath = spritesheetfolder + this.SpriteSheetName + "/spritesheet";
        var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        if (sprites.Count() == 0)
        {
            spritesheetfilepath = spritesheetfolder + "chara_01/spritesheet";
            sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        }

        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the sprite sheet in case it is changed later
        this.LoadedSpriteSheetName = this.SpriteSheetName;
    }*/
}
