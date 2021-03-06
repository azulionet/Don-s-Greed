﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class PlayerDungeon : Player
{
	void Start()
	{
		if (CSceneMng.Ins.eNowScene == eScene.Dungeon)
		{
			m_fpFixedUpdate = FixedUpdate_InDungeon;
			m_fpOnTriggerEnter2D = OnTriggerEnter2D_InDungeon;
			m_fpOnCollisionEnter2D = OnCollisionEnter2D_InDungeon;
		}
	}

	public void FixedUpdate_InDungeon()
	{
		if (this.m_rb.isKinematic == true) { return; }

		if (Input.GetAxis("Vertical") < 0 && Input.GetButton("Jump"))
		{
			var pos = transform.localPosition;
			pos.y -= 20;
			transform.localPosition = pos;
			// 
			// 			m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
			// 			m_rb.AddForce(new Vector2(0, -10.0f));


		}
		else if (Input.GetButton("Jump") == true)
		{
			if (m_refStat.nNowJumpCount < m_refStat.nMaxJump)
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
				m_rb.AddForce(new Vector2(0, m_refStat.fJumpForce));

				// 				var pos = transform.localPosition;
				// 				pos.y += 15.0f;
				// 				transform.localPosition = pos;
			}

			// m_rb.isKinematic = true;
		}
		else
		{
			// m_rb.isKinematic = false;
		}

		bool bGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
		if (bGround == true)
		{
			this.m_refStat.nNowJumpCount = 0;
		}

		m_bGrounded = bGround;

		float f = Input.GetAxis("Horizontal");
		f /= 2;

		if ((f > 0 && m_bRight == false) || (f < 0 && m_bRight == true))
		{
			m_bRight = !m_bRight;

			m_spr.flip = (m_bRight) ? UIBasicSprite.Flip.Nothing : UIBasicSprite.Flip.Horizontally;
		}

		m_rb.velocity = new Vector2(f * m_refStat.fMove, m_rb.velocity.y);
	}

	public void OnTriggerEnter2D_InDungeon(Collider2D collision)
	{
		if( this.m_rb.isKinematic == true ) { return; }

		InteractionObj obj = collision.gameObject.GetComponent<InteractionObj>();

		if (obj == null)
		{
			return;
		}

		obj.Interaction(this);
	}

	public void OnCollisionEnter2D_InDungeon(Collision2D collision)
	{
		if (this.m_rb.isKinematic == true) { return; }
	}
}
