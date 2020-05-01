using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HamsterController : MonoBehaviour
{
    private Camera cam;
    public GameObject SelectedItemBubble;
    //public AudioClip DeselectCurrentItemSound;
    public float MovementSpeed = 2.5f;

    bool hasScreenBeenTouchedInLastFrame;
    bool hasScreenBeenClickedInLastFrame;
    InventoryItemSlot slot;

    //[HideInInspector]
    public int HamsterMovementCounter;
    Vector2 HamsterGoalPosition;

    HamsterAnimationController animationController;
    bool isWalkingTowardsInteractable = false;
    GameObject interactableToActivateOnArrival = null;

    int clickCount = 0;

    private float doubleTapTime = 0.5f;
    private int taps = 0;

    void Awake()
    {
        SetHamsterAtPosition(GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>().GetStartPosition());
    }

    void Start()
    {
        cam = Camera.main;
        hasScreenBeenTouchedInLastFrame = true;
        hasScreenBeenClickedInLastFrame = true;
        HamsterMovementCounter = 0;

        animationController = GetComponent<HamsterAnimationController>();

    }

    void Update()
    {
        //check for hole in dotties room after hole
        if (GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>().isScene_CurrentlyLoaded("RoomOliveAfterHole") && HamsterGoalPosition.x > 0.4)
        {
            HamsterGoalPosition.x = 0.4f;
        }
        else if (HamsterGoalPosition.x > 7.5)
        {
            HamsterGoalPosition.x = 7.5f;
        }

        else if (HamsterGoalPosition.x < -7.5)
        {
            HamsterGoalPosition.x = -7.5f;
        }


        if (Input.GetMouseButtonDown(0))
        {
            clickCount += 1;
        }
        else
        {
            clickCount = 0;
        }

#if UNITY_ANDROID
        //Input Management
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (IsHamsterMovementEnabled() && !hasScreenBeenTouchedInLastFrame)
            {
                Vector2 touchPosition = cam.ScreenToWorldPoint(touch.position);
                touchPosition.y = transform.position.y;
                SetHamsterGoalPosition(touchPosition);
            }

            else if (touch.phase == TouchPhase.Began && GameObject.Find("Interactions").GetComponent<InteractionMenu>().GetIsMenuOpen())
            {
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    GameObject.Find("Interactions").GetComponent<InteractionMenu>().CloseInteractions();
                }
            }

            hasScreenBeenTouchedInLastFrame = true;
        }
        else
        {
            hasScreenBeenTouchedInLastFrame = false;
        }
#endif
#if !UNITY_ANDROID
        if (clickCount > 0)
        {
            if (!hasScreenBeenClickedInLastFrame && IsHamsterMovementEnabled())
            {
                Vector2 clickPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.y = transform.position.y;
                SetHamsterGoalPosition(clickPosition);
            }
            else if (!hasScreenBeenClickedInLastFrame && GameObject.Find("Interactions").GetComponent<InteractionMenu>().GetIsMenuOpen())
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    GameObject.Find("Interactions").GetComponent<InteractionMenu>().CloseInteractions();
                }
            }

            hasScreenBeenClickedInLastFrame = true;
        }
        else
        {
            hasScreenBeenClickedInLastFrame = false;
        }
#endif


        //Hamster Movement
        float velocity = 0.0f;

        if (!isWalkingTowardsInteractable && Mathf.Abs(HamsterGoalPosition.x - transform.position.x) < 0.1)
        {
            velocity = 0;
            animationController.Stand();
        }

        else if (isWalkingTowardsInteractable && Mathf.Abs(HamsterGoalPosition.x - transform.position.x) < 3)
        {
            velocity = 0;
            animationController.Stand();
            GameObject.Find("Interactions").GetComponent<InteractionMenu>().StartInteraction(interactableToActivateOnArrival.GetComponent<EventHandler>().graph);
            isWalkingTowardsInteractable = false;
            HamsterGoalPosition = transform.position;
        }

        else if (HamsterGoalPosition.x < transform.position.x)
        {
            animationController.LookLeft();
            velocity = -1;
            animationController.Walk();
        }
        else if (HamsterGoalPosition.x > transform.position.x)
        {
            animationController.LookRight();
            velocity = 1;
            animationController.Walk();
        }

        if (velocity != 0 && animationController.GetIsWalking())
        {
            Vector3 direction = new Vector3(velocity, 0, 0);
            transform.Translate(direction * MovementSpeed * Time.deltaTime, Space.World);
        }
    }

    //double tap or double click required for opening inventory
    public void OnMouseDown()
    {
        if (taps == 0)
        {
            taps = 1;
            StartCoroutine("tapTimer");
        }

        else if (taps == 1)
        {
            toggleInventory();
            taps = 0;
        }
    }

    private IEnumerator tapTimer()
    {
        yield return new WaitForSeconds(doubleTapTime);
        taps = 0;
    }

    void toggleInventory()
    {
        InventoryController inventory = GameObject.Find("Inventory").GetComponent<InventoryController>();

        if (IsHamsterMovementEnabled())
        {
            inventory.OpenInventory("Use");
        }
    }

    public void SetItemAsSelected(InventoryItemSlot _slot)
    {
        DisableHamsterMovement();
        slot = _slot;
        Sprite sprite = slot.GetItemSprite();
        SelectedItemBubble.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        SelectedItemBubble.SetActive(true);
        slot.StartEvent();
    }

    public void DeselectCurrentItem()
    {
        //GetComponent<AudioSource>().PlayOneShot(DeselectCurrentItemSound);
        SelectedItemBubble.SetActive(false);
        EnableHamsterMovement();
    }

    public bool IsHamsterMovementEnabled()
    {
        return HamsterMovementCounter == 0;
    }

    public void EnableHamsterMovement()
    {
        HamsterMovementCounter -= 1;
    }

    public void DisableHamsterMovement()
    {
        HamsterMovementCounter += 1;
    }

    public void SetHamsterGoalPosition(Vector2 newPosition)
    {
        HamsterGoalPosition = newPosition;
        isWalkingTowardsInteractable = false;
    }

    public void SetHamsterGoalInteractable(GameObject interactable)
    {
        InteractionMenu menu = GameObject.Find("Interactions").GetComponent<InteractionMenu>();

        if (!IsHamsterMovementEnabled())
        {
            return;
        }

        Vector3 newGoalPosition = interactable.transform.position;

        hasScreenBeenClickedInLastFrame = true;
        hasScreenBeenTouchedInLastFrame = true;
        HamsterGoalPosition = newGoalPosition;
        interactableToActivateOnArrival = interactable;
        isWalkingTowardsInteractable = true;
    }

    public void SetHamsterAtPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        SetHamsterGoalPosition(new Vector2(newPosition.x, newPosition.y));
    }

}
